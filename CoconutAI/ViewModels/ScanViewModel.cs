using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace CoconutAI.ViewModels
{
    internal class ScanViewModel
    {
        public ICommand ScanCommand { get; }

        public ScanViewModel()
        {
            ScanCommand = new AsyncRelayCommand(Scan);
        }

        async Task Scan()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                }
            }
        }
    }
}
