using System;
using System.Text;
using System.Net;
using System.IO;

class SaveCourseBookInfo
{
    static void Main(string[] args)
    {
        string fileName = "CourseList.html";
        string insertFileName = "../Scripts/InsertDepartment.sql";
        string line = "";
        string courseName = "";
        //string fileSuffix = ".html";

        try
        {
            //WebClient webAccess = new WebClient();
            StreamReader inputFile = new StreamReader(fileName);
            StreamWriter outputFile = new StreamWriter(insertFileName);
            line = inputFile.ReadLine();
            while (line != null)
            {
                if (line.Contains("<a") && line.Contains("</a>"))
                {
                    if (!line.Contains("body"))
                    {
                        courseName = line.Remove(line.LastIndexOf('<')).Substring(line.IndexOf('>') + 1);
                        outputFile.WriteLine("INSERT INTO dbo.Department (Department) VALUES (\'" + courseName.Trim() + "\')");
                        //webAccess.DownloadFile("http://w4.bookstore.ubc.ca/students/guide/" + courseName + fileSuffix, "BookListByCourse\\" + courseName + fileSuffix);
                    }
                }
                line = inputFile.ReadLine();
            }
            inputFile.Close();
            outputFile.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
