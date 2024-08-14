using Melody.Abstractions;
using Melody_Windows.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TagLib.Matroska;
using TagLib;
using Windows.Media.MediaProperties;
using Windows.Media.Transcoding;
using Windows.Storage;
using Windows.Storage.Streams;
using YoutubeExplode.Videos.Streams;
using Melody;
using YoutubeExplode.Common;

namespace Melody_Windows.Downloads
{
    public static class Downloader
    {
        public static string DownloadPath = "C:\\Users\\u1ben\\Downloads";
        public async static Task<StorageFile> Download(DownloadItem download, CancellationToken cancelToken = default)
        {
            var media = download.Media;
            download.Status = "Getting stream";
            var stream = await media.GetStream();

            var outputfile = await (await StorageFolder.GetFolderFromPathAsync(DownloadPath))
            .CreateFileAsync($"{media.Name.MakeSafeForFiles()}.mp3", CreationCollisionOption.ReplaceExisting);
            var tempfile = await ApplicationData.Current.TemporaryFolder
                .CreateFileAsync($"{media.Name.MakeSafeForFiles()}.mp3temp", CreationCollisionOption.ReplaceExisting);

            try
            {
                download.Status = "Downloading";
                using (var filestream = await tempfile.OpenStreamForWriteAsync())
                {
                    // No need to seek to the beginning if the stream is already at position 0
                    await stream.CopyToAsync(filestream, 81920, download.Progress, cancelToken);

                    await filestream.FlushAsync(CancellationToken.None);
                }

                await tempfile.ConvertToMP3Async(outputfile);
                await tempfile.TryDeleteAsync();

                download.Status = "Success";
                return outputfile;
            }
            catch (System.Net.Http.HttpRequestException)
            {
                download.Status = "Error";
                return null;
            }
            finally
            {
                stream?.Dispose();
            }

        }
        public static async Task CopyToAsync(this Stream source, Stream destination, int bufferSize, IProgress<long> progress = default, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);
            if (!source.CanRead)
                throw new ArgumentException("Has to be readable", nameof(source));
            if (!destination.CanWrite)
                throw new ArgumentException("Has to be writable", nameof(destination));

            var length = source.Length;
            var buffer = new byte[bufferSize];
            long totalBytesRead = 0;
            int bytesRead;
            while ((bytesRead = await source.ReadAsync(buffer, cancellationToken).ConfigureAwait(false)) != 0)
            {
                await destination.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken).ConfigureAwait(false);
                totalBytesRead += bytesRead;
                progress?.Report(totalBytesRead/length);
            }
        }
        public static async Task<StorageFile> ConvertToMP3Async(this StorageFile source, StorageFile destination)
        {
            var profile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);
            var transcoder = new MediaTranscoder();

            PrepareTranscodeResult prepareOp = await
                transcoder.PrepareFileTranscodeAsync(source, destination, profile);

            if (prepareOp.CanTranscode)
            {
                await prepareOp.TranscodeAsync();
            }

            return destination;
        }
        public static async Task<bool> TryDeleteAsync(this StorageFile file)
        {
            try
            {
                await file.DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static async Task<StorageFile> SetMetadata(this StorageFile file, SingleMedia media)
        {
            StorageFileAbstraction taglibfile = new StorageFileAbstraction(file);
            using (var tagFile = TagLib.File.Create(taglibfile, ReadStyle.Average))
            {
                //read the raw tags
                tagFile.Tag.Title = media.Title;
                tagFile.Tag.Performers = media.Authors.Select(author => author.Name).ToArray();
                if(media is SpotifyTrack track)
                {
                    tagFile.Tag.Album = track.Album;
                    tagFile.Tag.Track = (uint)track.TrackNumber;
                }
                tagFile.Tag.Year = (uint)media.Date.Year;

                try
                {
                    //Set cover art
                    RandomAccessStreamReference random = RandomAccessStreamReference.CreateFromUri(new Uri(media.ImageURL,UriKind.Absolute));

                    using (IRandomAccessStream stream = await random.OpenReadAsync())
                    {
                        using (var TempStream = stream.AsStream())
                        {
                            TempStream.Position = 0;
                            TagLib.Picture pic = new TagLib.Picture();
                            pic.Data = ByteVector.FromStream(TempStream);
                            pic.Type = PictureType.FrontCover;

                            tagFile.Tag.Pictures = new IPicture[] { pic };
                        }
                    }
                }
                catch
                {

                }

                //Save and dispose
                tagFile.Save();
                tagFile.Dispose();
            }
            return file;
        }
        public enum DownloadResult
        {
            Success, Error, Cancelled
        }
    }
}
