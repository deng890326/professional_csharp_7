using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace WindowsAppEditor
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public async Task OnOpen()
        {
            try
            {
                FileOpenPicker fileOpenPicker = new FileOpenPicker()
                {
                    ViewMode = PickerViewMode.Thumbnail,
                    SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                };
                fileOpenPicker.FileTypeFilter.Add(".txt");
                fileOpenPicker.FileTypeFilter.Add(".md");

                StorageFile storageFile = await fileOpenPicker.PickSingleFileAsync();
                if (storageFile != null)
                {
                    using (IRandomAccessStreamWithContentType stream = await storageFile.OpenReadAsync())
                    using (DataReader reader = new DataReader(stream))
                    {
                        await reader.LoadAsync((uint)stream.Size);
                        text1.Text = reader.ReadString((uint)stream.Size);
                    }
                }
            }
            catch (Exception ex)
            {
                _ = new MessageDialog(ex.Message, "Error").ShowAsync();
            }
        }

        public async Task OnSave()
        {
            try
            {
                FileSavePicker fileSavePicker = new FileSavePicker()
                {
                    SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                    SuggestedFileName = "New Document"
                };
                fileSavePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
                StorageFile file = await fileSavePicker.PickSaveFileAsync();
                if (file != null)
                {
                    using (StorageStreamTransaction tx = await file.OpenTransactedWriteAsync())
                    using (DataWriter writer = new DataWriter(tx.Stream))
                    {
                        tx.Stream.Seek(0);
                        writer.WriteString(text1.Text);
                        tx.Stream.Size = await writer.StoreAsync();
                        await tx.CommitAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _ = new MessageDialog(ex.Message, "Error").ShowAsync();
            }
        }
    }
}
