using System.Security.Cryptography;
using System.Text;

internal class Program : IDisposable
{
    private CngKey aliceKeySignature;
    private byte[] alicePublicKeyBlob;
    
    private static void Main(string[] args)
    {
        using Program program = new Program();
        program.Run();
    }

    public void Run()
    {
        byte[] alice = Encoding.UTF8.GetBytes("Alice");
        byte[] signature = CreateSignature(alice);
        Console.WriteLine($"signature: {Convert.ToBase64String(signature)}");
        Console.WriteLine($"verify signature: {VerifySignature(alice, signature)}");
    }

    public Program()
    {
        aliceKeySignature = CngKey.Create(CngAlgorithm.ECDsaP256);
        alicePublicKeyBlob = aliceKeySignature.Export(CngKeyBlobFormat.GenericPublicBlob);
    }

    private byte[] CreateSignature(byte[] data)
    {
        using ECDsaCng eCDsaCng = new ECDsaCng(aliceKeySignature);
        return eCDsaCng.SignData(data, HashAlgorithmName.SHA256);
    }

    private bool VerifySignature(byte[] data, byte[] signature)
    {
        using CngKey cngKey = CngKey.Import(alicePublicKeyBlob, CngKeyBlobFormat.GenericPublicBlob);
        using ECDsaCng eCDsaCng = new ECDsaCng(cngKey);
        return eCDsaCng.VerifyData(data, signature, HashAlgorithmName.SHA256);
    }

    public void Dispose()
    {
        aliceKeySignature.Dispose();
    }
}