using StudentMathTestSystem.Interfaces;
using System;
using System.Xml.Linq;

namespace StudentMathTestSystem.Utilities
{
    public class XmlValidationService : IXmlValidationService
    {
        public string ValidateXml(string xmlContent)
        {
            try
            {
                var xDocument = XDocument.Parse(xmlContent);

                var teacherElement = xDocument.Element("Teacher");
                if (teacherElement == null)
                    return "Root element <Teacher> is missing.";

                if (teacherElement.Attribute("ID") == null)
                    return "<Teacher> element is missing the required attribute 'ID'.";

                var studentsElement = teacherElement.Element("Students");
                if (studentsElement == null)
                    return "<Students> node is missing under <Teacher>.";

                foreach (var studentElement in studentsElement.Elements("Student"))
                {
                    if (studentElement.Attribute("ID") == null)
                        return "<Student> element is missing the required attribute 'ID'.";

                    var exams = studentElement.Elements("Exam");
                    if (!exams.Any())
                        return $"No <Exam> nodes found for <Student ID='{studentElement.Attribute("ID").Value}'.";

                    foreach (var examElement in exams)
                    {
                        if (examElement.Attribute("Id") == null)
                            return "<Exam> element is missing the required attribute 'Id'.";

                        var tasks = examElement.Elements("Task");
                        if (!tasks.Any())
                            return $"No <Task> nodes found for <Exam Id='{examElement.Attribute("Id").Value}'.";

                        foreach (var taskElement in tasks)
                        {
                            if (taskElement.Attribute("id") == null)
                                return "<Task> element is missing the required attribute 'id'.";

                            if (string.IsNullOrWhiteSpace(taskElement.Value))
                                return $"<Task id='{taskElement.Attribute("id").Value}'> is missing content.";
                        }
                    }
                }

                return "Valid";
            }
            catch (Exception ex)
            {
                return $"Error parsing XML: {ex.Message}";
            }
        }
    }
}
