using Microsoft.VisualBasic;
using NAudio.Wave;
using AutoSerialNumber;
namespace NAudioExmple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var timerSpan = DateTime.Now - DateTime.Now;
            var timerSpanA = DateTime.Now > DateTime.Now;
            //var IsTrue = DateTime.Now < timerSpanA;

            //设置录音文件和保存路径
            //string outputFilePath = "Recorded Audio" + DateTime.Now.ToString("hh''mm''ss-") + DateTime.Now.ToString("fff") +".wav";
            string outputFilePath = "Recorded Audio";
            CreateSerialNumber createSerialNumber = new CreateSerialNumber();
            outputFilePath = createSerialNumber.Create(outputFilePath,".mp3");

            // 创建一个WaveInEvent实例，用于从默认录音设备捕获音频
            using (var waveIn = new WaveInEvent())
            {
                // 设置音频格式（例如，16位PCM，44.1kHz，立体声）
                waveIn.WaveFormat = new WaveFormat(44100, 2);

                // 创建一个WaveFileWriter实例，用于将捕获的音频写入文件
                using (var waveFile = new WaveFileWriter(outputFilePath, waveIn.WaveFormat))
                {
                    // 订阅DataAvailable事件，该事件在捕获到音频数据时触发
                    waveIn.DataAvailable += (s, e) =>
                    {
                        // 将捕获到的音频数据写入文件
                        waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                        // 可选：在控制台上显示录音进度
                        Console.WriteLine($"{e.BytesRecorded} bytes recorded.");
                    };

                    // 订阅RecordingStopped事件，该事件在录音停止时触发
                    waveIn.RecordingStopped += (s, e) =>
                    {
                        Console.WriteLine("Recording stopped.");
                    };

                    // 开始录音
                    waveIn.StartRecording();
                    Console.WriteLine("recording...");
                    // 等待用户输入，然后按Enter键停止录音
                    Console.WriteLine("Press Enter to stop recording...");
                    Console.ReadLine();

                    // 停止录音
                    waveIn.StopRecording();
                }
            }
            Console.WriteLine($"Recording saved to {outputFilePath}");
        }
    }
}
