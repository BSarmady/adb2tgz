using System;
using System.IO;

static class Program {

    static int Main(string[] args) {
        try {

            Console.WriteLine("Converts Android adb backup file to tgz archive type");
            if (args.Length > 0) {
                if (File.Exists(args[0])) {
                    Console.WriteLine("using file " + args[0]);

                    FileStream fs = new FileStream(args[0], FileMode.Open, FileAccess.ReadWrite);
                    byte[] buffer = new byte[8] { 0x1F, 0x8B, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    byte[] buffer1 = new byte[1024 * 1024 * 10];
                    fs.Read(buffer1, 0, 8);
                    //Avoid corrupting a file that is already converted
                    if (Compare(buffer, buffer1)) {
                        Console.WriteLine("Selected file is already a tgz type. it might have been converted before");
                        return 1;
                    }


                    fs.Position = 0;
                    fs.Write(buffer, 0, 8);
                    long size = fs.Length;
                    fs.Position = 24;
                    int notification = 10;
                    while (fs.Position < fs.Length) {
                        fs.Read(buffer1, 0, buffer1.Length);
                        fs.Position = fs.Position - buffer1.Length - 16;
                        fs.Write(buffer1, 0, buffer1.Length);
                        fs.Position = fs.Position + 16;
                        notification--;
                        if (notification < 1) {
                            Console.WriteLine("  " + ((fs.Position / (decimal) size) * 100).ToString("0") + "%");
                        }
                    }

                    fs.Flush();
                    fs.Close();
                    Console.WriteLine("  Renaming adb file to tgz");
                    File.Move(args[0], args[0].Replace(Path.GetExtension(args[0]), ".tgz"));
                }
            } else {
                Console.WriteLine("No backup file selected, drag your backup file on executable to convert it to tar file");
            }
        } catch (Exception ex) {
            Console.WriteLine(ex.ToString());
        }

        Console.WriteLine("Complete. Press a key to exit");
        Console.ReadKey();
        return 0;
    }

    private static bool Compare(byte[] buffer, byte[] buffer1) {
        for (int i = 0;i < buffer.Length;i++) {
            if (buffer[i] != buffer1[i])
                return false;
        }
        return true;
    }
}