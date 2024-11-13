namespace AutoSerialNumber
{
    public class CreateSerialNumber
    {
        int serialIndex;
        public string Create(string applicationPath, string suffix)
        {
            int index=serialIndex;
            string filePath = applicationPath;
            bool isExists = true;
            while (isExists)
            {
                filePath = $"{applicationPath}_{index}" + suffix;
               // if (!File.Exists(filePath)) break;
                isExists = File.Exists(filePath);
                serialIndex++;
                index = serialIndex;
            }
            return filePath;
        }
    }
}
