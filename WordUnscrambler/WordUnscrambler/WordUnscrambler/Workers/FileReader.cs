using System;
using System.IO;

namespace WordUnscrambler.Workers
{
    class FileReader
    {
        public string[] Read(string filename)
        {
            //Creates a string array
            string[] fileContent;
            try
            {
                //Reads words from file
                fileContent = File.ReadAllLines(filename);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return fileContent;
        }
    }
}
