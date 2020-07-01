using System;
using System.IO;
using System.Text;

namespace Tweet_info
{
    public class FileCreator
    {
        string root = "TweetContentHolder";
        string main_path = Directory.GetCurrentDirectory();

        public FileCreator() {

            if (!Directory.Exists(root)) {
                Directory.CreateDirectory(main_path + "/" + root);
            }
        }

        public void createFile(string file_name, string fileContent) {

            try {
                TextWriter writer = new StreamWriter(main_path + "/" + root + "/" + file_name + ".txt",true);
                writer.Write(fileContent);
                writer.Close();

            }catch (Exception e) { Console.WriteLine(e); }
        }
    }
}
