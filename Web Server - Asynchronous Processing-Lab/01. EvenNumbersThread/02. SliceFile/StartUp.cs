using System;
using System.IO;
using System.Threading.Tasks;

namespace _02._SliceFile
{
    class StartUp
    {
        private const int BufferSize = 4096;
        static void Main(string[] args)
        {
            ReadAndProcessCommand();
            while (true)
            {
                Console.WriteLine("Anything else?");
                var answer = ReadAndProcessCommand();
                if (!answer)
                {
                    break;
                }
            }
        }

        static bool ReadAndProcessCommand()
        {
            var videoPath = Console.ReadLine();
            if (videoPath == "")
            {
                return false;
            }
            var destination = Console.ReadLine();
            var pieces = int.Parse(Console.ReadLine());

            SliceAsync(videoPath, destination, pieces);

            return true;
        }

        static void Slice(string sourceFile, string destinationPath, int parts)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            using (var reader = new FileStream(sourceFile, FileMode.Open))
            {
                var extension = sourceFile.Substring(sourceFile.LastIndexOf('.') + 1);

                long pieceSize = (long)Math.Ceiling((double)reader.Length / parts);

                for (int i = 1; i <= parts; i++)
                {
                    var currentPieceSize = 0;
                    var currentPart = $"{destinationPath}/Part-{i}.{extension}";

                    using (var writer = new FileStream(currentPart, FileMode.Create))
                    {
                        byte[] buffer = new byte[BufferSize];
                        while (reader.Read(buffer, 0, BufferSize) == BufferSize)
                        {
                            writer.Write(buffer, 0, BufferSize);
                            currentPieceSize += BufferSize;

                            if (currentPieceSize >= pieceSize)
                            {
                                break;
                            }
                        }
                    }
                }

                Console.WriteLine("Slice Complete.");
            }
        }

        static void SliceAsync(string sourceFile,
            string destinationPath, int parts)
        {
            Task.Run(() =>
            {
                Slice(sourceFile, destinationPath, parts);
            });
        }
    }
}
