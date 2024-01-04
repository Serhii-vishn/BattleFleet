namespace BattleFleet.src.FileManager
{
    class ShipTemplateManager : FileManager
    {
        private List<string> templates;
        private string templatesFilePath;

        public ShipTemplateManager():base()
        {
            templatesFilePath = Path.Combine(savesFolderPath, "Templates.txt");
            templates = new List<string>();
        }

        public void SaveTemplate(List<string> template, string templateName)
        {
            try
            {
                using (FileStream fileStream = new FileStream(templatesFilePath, FileMode.Append, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine(templateName);
                    foreach (var str in template)
                    {
                        writer.WriteLine(str);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error saving chat history: " + ex.Message);
            }
        }

        //public List<string> GetTemplateNames()
        //{

        //}

        public string GetTemplateByName(string templateName)
        {
            string template = string.Empty;
            try
            {
                using (FileStream fileStream = new FileStream(templatesFilePath, FileMode.Append, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fileStream))
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving chat history: " + ex.Message);
            }
            return template;
        }
    }
}