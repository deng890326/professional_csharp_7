using System.Security.Cryptography;
using System.Text;

namespace SecureTransfer
{
    internal class Program : IDisposable
    {
        private CngKey aliceKey;
        private CngKey bobKey;
        private byte[] alicePublicKeyBlob;
        private byte[] bobPublicKeyBlob;

        static async Task Main(string[] args)
        {
            using var p = new Program();
            byte[] data = await p.AliceSendDataAsync("This is a secret message for Bob.");
            await p.BobReceiveDataAsync(data);
            Console.ReadKey();
        }

        public Program()
        {
            aliceKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            bobKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP256);
            alicePublicKeyBlob = aliceKey.Export(CngKeyBlobFormat.EccPublicBlob);
            bobPublicKeyBlob = bobKey.Export(CngKeyBlobFormat.EccPublicBlob);
        }

        public async Task<byte[]> AliceSendDataAsync(string message)
        {
            Console.WriteLine($"Alice sending: {message}");
            using var aliceAlgorithm = new ECDiffieHellmanCng(aliceKey);
            using var bobPublicKey = CngKey.Import(bobPublicKeyBlob, CngKeyBlobFormat.EccPublicBlob);
            byte[] symmKey = aliceAlgorithm.DeriveKeyMaterial(bobPublicKey);
            Console.WriteLine($"{nameof(symmKey)}: {Convert.ToBase64String(symmKey)}");
            using var aes = Aes.Create();
            aes.Key = symmKey;
            aes.GenerateIV();
            Console.WriteLine($"{nameof(aes.IV)}: {Convert.ToBase64String(aes.IV)}");
            
            using var memoryStream = new MemoryStream();
            await memoryStream.WriteAsync(aes.IV, 0, aes.IV.Length);

            using (var encryptor = aes.CreateEncryptor())
            using (var crytoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                await crytoStream.WriteAsync(data, 0, data.Length);
            }

            return memoryStream.ToArray();
        }

        public async Task BobReceiveDataAsync(byte[] encrytedData)
        {
            Console.WriteLine($"Bob Receive: {Convert.ToBase64String(encrytedData)}");
            using var bobAlgorithm = new ECDiffieHellmanCng(bobKey);
            using var alicePublicKey = CngKey.Import(alicePublicKeyBlob, CngKeyBlobFormat.EccPublicBlob);
            byte[] symmKey = bobAlgorithm.DeriveKeyMaterial(alicePublicKey);
            Console.WriteLine($"{nameof(symmKey)}: {Convert.ToBase64String(symmKey)}");
            using var aes = Aes.Create();
            aes.Key = symmKey;
            int nByte = aes.BlockSize / 8;
            var iv = new byte[nByte];
            Array.Copy(encrytedData, 0, iv, 0, nByte);
            aes.IV = iv;
            Console.WriteLine($"{nameof(aes.IV)}: {Convert.ToBase64String(aes.IV)}");
            
            using var memoryStream = new MemoryStream();

            using (var decryptor = aes.CreateDecryptor())
            using (var crytoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
            {
                await crytoStream.WriteAsync(encrytedData, nByte, encrytedData.Length - nByte);
            }

            string message = Encoding.UTF8.GetString(memoryStream.ToArray());
            Console.WriteLine($"decrypted message: {message}");
        }

        public void Dispose()
        {
            aliceKey?.Dispose();
            bobKey?.Dispose();
        }
    }
}