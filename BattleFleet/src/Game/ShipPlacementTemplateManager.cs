using System;

namespace BattleFleet.src.Game
{
    class ShipPlacementTemplateManager
    {
        private List<string> templates;
        private string templatesFolderPath;

        public ShipPlacementTemplateManager()
        {
            this.templatesFolderPath = Path.Combine("saves", "Templates.txt");
            templates = new List<string>();
        }

        public void SaveTemplate(string template, string templateName)
        {
            try
            {
                using (FileStream fileStream = new FileStream(templatesFolderPath, FileMode.Append, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine(template);
                    writer.WriteLine(templateName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving chat history: " + ex.Message);
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
                using (FileStream fileStream = new FileStream(templatesFolderPath, FileMode.Append, FileAccess.Write))
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