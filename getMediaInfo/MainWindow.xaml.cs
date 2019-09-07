using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediaInfoLib;

namespace getMediaInfo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaInfo mediaInfo = new MediaInfo();

        private string filePath;

        private string fileName;

        private string fileName2;//后缀名

        private string fileDuration;

        private string fileFormat;

        private string fileSize;

        private string fileBitrate;

        private string videoFormat;

        private string videoHeight;

        private string videoWidth;

        private string videoCodecProfile;

        private string videoFrameRate;

        private string audioBitRate;

        private string audioChannel;

        private string audioCodec;

        private string audioLanguage;

        private string releaseTime;

        private string subtitleText;

        private string subtitleText2;

        private int fileInfoCopy = 0;

        public MainWindow()
        {
            InitializeComponent();
            base.ResizeMode = ResizeMode.CanMinimize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            RTB_MediaInfo.AllowDrop = true;
            RTB_MediaInfo.AddHandler(RichTextBox.DragOverEvent, new DragEventHandler(RTB_MediaInfo_DragOver), true);
            RTB_MediaInfo.AddHandler(RichTextBox.DropEvent, new DragEventHandler(RTB_MediaInfo_Drop), true);
        }

        private void handleMediaInfo()
        {
            mediaInfo.Open(filePath);
            fileName = mediaInfo.Get(StreamKind.General, 0, "FileName");//RELEASE.NAME
            fileName2 = mediaInfo.Get(StreamKind.General, 0, "FileExtension");//RELEASE.NAME
            fileSize = mediaInfo.Get(StreamKind.General, 0, "FileSize/String");//RELEASE.SIZE
            fileFormat = mediaInfo.Get(StreamKind.General, 0, "Format");//RELEASE.FORMAT
            fileDuration = mediaInfo.Get(StreamKind.General, 0, "Duration/String3");//DUTATION
            fileBitrate = mediaInfo.Get(StreamKind.General, 0, "OverallBitRate/String");//OVERALL.BITRATE
            videoHeight = mediaInfo.Get(StreamKind.Video, 0, "Height");//RESOLUTION.Height
            videoWidth = mediaInfo.Get(StreamKind.Video, 0, "Width");//RESOLUTION.Width
            videoFrameRate = mediaInfo.Get(StreamKind.Video, 0, "FrameRate");//FRAME.RATE
            videoFormat = mediaInfo.Get(StreamKind.Video, 0, "Format");//FORMAT

            if(videoFormat == "hev1")
            {
                videoFormat = "HEVC Main@L5@Main";
            }

            videoCodecProfile = mediaInfo.Get(StreamKind.Video, 0, "Format_Profile");
            audioLanguage = mediaInfo.Get(StreamKind.Audio, 0, "Language/String");

            if(audioLanguage == "")
            {
                audioLanguage = "Chinese";
            }
            if(audioLanguage == "en")
            {
                audioLanguage = "English";
            }

            audioCodec = mediaInfo.Get(StreamKind.Audio, 0, "Codec");
            audioChannel = mediaInfo.Get(StreamKind.Audio, 0, "Channel(s)/String");
            audioBitRate = mediaInfo.Get(StreamKind.Audio, 0, "BitTate/String");
            releaseTime = mediaInfo.Get(StreamKind.General, 0, "File_Created_Date_Local");

            subtitleText = mediaInfo.Get(StreamKind.Text, 0, "Language/String1");
            subtitleText2 = mediaInfo.Get(StreamKind.Text, 0, "Format");

        }

        private void showMediaInfo()
        {
            RTB_MediaInfo.AppendText("[IMG][/IMG]\n\n\n\n\n");
            RTB_MediaInfo.AppendText("[IMG]https://images2.imgbox.com/04/6b/Ggp5ReQb_o.png[/IMG]\n\n");
            RTB_MediaInfo.AppendText("[font=Courier][quote]");
            RTB_MediaInfo.AppendText("[size=3][color=RoyalBlue][b]★★★★★ General Information ★★★★★[/b][/color][/size]\n\n");
            RTB_MediaInfo.AppendText("RELEASE.NAME...: " + fileName + "." + fileName2 + "\n");
            RTB_MediaInfo.AppendText("RELEASE.DATE...: " + releaseTime + "\n");
            RTB_MediaInfo.AppendText("RELEASE.SIZE...: " + fileSize + "\n");
            RTB_MediaInfo.AppendText("RELEASE.FORMAT.: " + fileFormat + "\n");
            RTB_MediaInfo.AppendText("DURATION.......: " + fileDuration + "(HH:MM:SS.MMM)\n");
            RTB_MediaInfo.AppendText("OVERALL.BITRATE: " + fileBitrate + "\n");
            RTB_MediaInfo.AppendText("RESOLUTION.....: " + videoWidth + "x" + videoHeight + "\n");
            RTB_MediaInfo.AppendText("VIDEO.CODEC....: " + videoFormat + " " + videoCodecProfile + "\n");
            RTB_MediaInfo.AppendText("FRAME.RATE.....: " + videoFrameRate + "FPS\n");
            if(audioBitRate != "")
            {
                RTB_MediaInfo.AppendText("AUDIO..........: " + audioLanguage + " " + audioCodec + " " + audioChannel + "@" + audioBitRate + "\n");
            }
            else
            {
                RTB_MediaInfo.AppendText("AUDIO..........: " + audioLanguage + " " + audioCodec + " " + audioChannel + "\n");
            }
            
            RTB_MediaInfo.AppendText("SUBTITLE.......: " + subtitleText + "/" + subtitleText2 + "\n");
            RTB_MediaInfo.AppendText("[/quote][/font]\n\n");
            RTB_MediaInfo.AppendText("[IMG]https://images2.imgbox.com/ce/e7/KCmGFMOB_o.png[/IMG]\n\n");
            RTB_MediaInfo.AppendText("[IMG][/IMG]");

        }

        private void RTB_MediaInfo_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.All;
            else
                e.Effects = DragDropEffects.None;
        }

        private void RTB_MediaInfo_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
            RTB_MediaInfo.Document.Blocks.Clear();

            if(e.Data.GetDataPresent(DataFormats.FileDrop, autoConvert: false))
            {
                string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
                filePath = array[0];
                handleMediaInfo();
                showMediaInfo();
            }
        }

        private void BTN_Copy_Click(object sender, RoutedEventArgs e)
        {
            if(fileInfoCopy == 1)
            {

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

    }
}

