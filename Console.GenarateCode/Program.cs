using Domain.Models;
using Domain.Querys.Base;
using Microsoft.VisualBasic.FileIO;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;


public class Program
{

    static void Main(string[] args)
    {
        
        Type entityType = typeof(QuyenSuDung);


        if (entityType != null)
        {
            // Gọi hàm tạo tự động nếu tìm thấy class
            GenarateTemplateMVC(entityType);
            Console.WriteLine("Tạo thành công");
        }
        else
        {
            Console.WriteLine("Không tìm thấy");
        }

        Console.ReadKey();
    }


    private static void GenarateTemplateMVC(Type listItemType)
        {
            string entityName = listItemType.Name;


            string outputDirectoryPath = $"Output/{entityName}";

            if (!Directory.Exists(outputDirectoryPath))
            {
                Directory.CreateDirectory(outputDirectoryPath);
            }
            GenarateTemplateIndex(entityName, listItemType);
            GenarateTemplateView(entityName, listItemType);
            GenarateTemplateEdit(entityName, listItemType);
            GenarateTemplateController(entityName, listItemType);
            GenarateTemplateIService(entityName, listItemType);
            GenarateTemplateService(entityName, listItemType);
            GenarateTemplateIRepository(entityName, listItemType);
            GenarateTemplateRepository(entityName, listItemType);
            GenarateTemplateRepository_Base(entityName, listItemType);
            GenarateTemplateEntity(entityName, listItemType);
            GenarateTemplateQuery(entityName, listItemType);


        Process.Start(new ProcessStartInfo
            {
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outputDirectoryPath),
                UseShellExecute = true
            });
    }
    private static void GenarateTemplateEntity(string entityName, Type listItemType)
    {
        var listPropertyInfo = listItemType.GetProperties();
        string outputDirectoryPath = $"Output/{entityName}";

        var entityNameRaw = entityName;


        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string templatesFolder = Path.Combine(projectRoot, "Templates");
        string inputFilePath = Path.Combine(templatesFolder, "Entity.txt");
        string outputFilePath = Path.Combine(outputDirectoryPath, $"{entityName}_Entity.cs");


        string fileContent = File.ReadAllText(inputFilePath);
        fileContent = fileContent.Replace("@EntityRaw@", entityNameRaw);

        File.WriteAllText(outputFilePath, fileContent);
    }
    private static void GenarateTemplateQuery(string entityName, Type listItemType)
    {
        var listPropertyInfo = listItemType.GetProperties();
        string outputDirectoryPath = $"Output/{entityName}";

        var entityNameRaw = entityName;


        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string templatesFolder = Path.Combine(projectRoot, "Templates");
        string inputFilePath = Path.Combine(templatesFolder, "Query.txt");
        string outputFilePath = Path.Combine(outputDirectoryPath, $"{entityName}Query.cs");


        string fileContent = File.ReadAllText(inputFilePath);
        fileContent = fileContent.Replace("@EntityRaw@", entityNameRaw);

        File.WriteAllText(outputFilePath, fileContent);
    }
    private static void GenarateTemplateController(string entityName, Type listItemType)
    {
        var listPropertyInfo = listItemType.GetProperties();
        string outputDirectoryPath = $"Output/{entityName}";

        var entityNameRaw = entityName;

        string outputFilePath = Path.Combine(outputDirectoryPath, $"{entityName}Controller.cs");


        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string templatesFolder = Path.Combine(projectRoot, "Templates");
        string inputFilePath = Path.Combine(templatesFolder, "Controller.txt");
        string fileContent = File.ReadAllText(inputFilePath);

        fileContent = fileContent.Replace("@EntityRaw@", entityNameRaw);

        File.WriteAllText(outputFilePath, fileContent);
    }
    private static void GenarateTemplateIService(string entityName, Type listItemType)
    {
        var listPropertyInfo = listItemType.GetProperties();
        string outputDirectoryPath = $"Output/{entityName}";

        var entityNameRaw = entityName;

        if (entityName.StartsWith("L"))
            entityName = entityName.Substring(1);

        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string templatesFolder = Path.Combine(projectRoot, "Templates");
        string inputFilePath = Path.Combine(templatesFolder, "IService.txt");
        string outputFilePath = Path.Combine(outputDirectoryPath, $"I{entityName}Service.cs");


        string fileContent = File.ReadAllText(inputFilePath);

        fileContent = fileContent.Replace("@Entity@", entityName);
        fileContent = fileContent.Replace("@EntityRaw@", entityNameRaw);

        File.WriteAllText(outputFilePath, fileContent);
    }

    private static void GenarateTemplateService(string entityName, Type listItemType)
    {
        var listPropertyInfo = listItemType.GetProperties();
        string outputDirectoryPath = $"Output/{entityName}";

        var entityNameRaw = entityName;

        if (entityName.StartsWith("L"))
            entityName = entityName.Substring(1);

        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string templatesFolder = Path.Combine(projectRoot, "Templates");
        string inputFilePath = Path.Combine(templatesFolder, "Service.txt"); 
        string outputFilePath = Path.Combine(outputDirectoryPath, $"{entityName}Service.cs");


        string fileContent = File.ReadAllText(inputFilePath);

        fileContent = fileContent.Replace("@Entity@", entityName);
        fileContent = fileContent.Replace("@EntityRaw@", entityNameRaw);

        File.WriteAllText(outputFilePath, fileContent);
    }


    private static void GenarateTemplateIRepository(string entityName, Type listItemType)
    {
        var listPropertyInfo = listItemType.GetProperties();
        string outputDirectoryPath = $"Output/{entityName}";

        var entityNameRaw = entityName;

        if (entityName.StartsWith("L"))
            entityName = entityName.Substring(1);

        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string templatesFolder = Path.Combine(projectRoot, "Templates");
        string inputFilePath = Path.Combine(templatesFolder, "IRepository.txt");
        string outputFilePath = Path.Combine(outputDirectoryPath, $"I{entityName}Repository.cs");


        string fileContent = File.ReadAllText(inputFilePath);

        fileContent = fileContent.Replace("@Entity@", entityName);
        fileContent = fileContent.Replace("@EntityRaw@", entityNameRaw);

        File.WriteAllText(outputFilePath, fileContent);
    }

    private static void GenarateTemplateRepository(string entityName, Type listItemType)
    {
        var listPropertyInfo = listItemType.GetProperties();
        string outputDirectoryPath = $"Output/{entityName}";

        var entityNameRaw = entityName;


        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string templatesFolder = Path.Combine(projectRoot, "Templates");
        string inputFilePath = Path.Combine(templatesFolder, "Repository.txt");
        string outputFilePath = Path.Combine(outputDirectoryPath, $"{entityName}Repository.cs");


        string fileContent = File.ReadAllText(inputFilePath);

        fileContent = fileContent.Replace("@EntityRaw@", entityNameRaw);

        File.WriteAllText(outputFilePath, fileContent);
    }
    private static void GenarateTemplateRepository_Base(string entityName, Type listItemType)
    {
        var listPropertyInfo = listItemType.GetProperties();
        string outputDirectoryPath = $"Output/{entityName}";

        var entityNameRaw = entityName;

        if (entityName.StartsWith("L"))
            entityName = entityName.Substring(1);

        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string templatesFolder = Path.Combine(projectRoot, "Templates");
        string inputFilePath = Path.Combine(templatesFolder, "Repository_Base.txt");
        string outputFilePath = Path.Combine(outputDirectoryPath, $"{entityName}Repository_Base.cs");


        string fileContent = File.ReadAllText(inputFilePath);

        fileContent = fileContent.Replace("@EntityRaw@", entityNameRaw);

        File.WriteAllText(outputFilePath, fileContent);
    }

    private static void GenarateTemplateIndex(string entityName, Type listItemType)
    {
        var listPropertyInfo = listItemType.GetProperties();
        string outputDirectoryPath = $"Output/{entityName}";


        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string templatesFolder = Path.Combine(projectRoot, "Templates");
        string inputFilePath = Path.Combine(templatesFolder, "Index.txt");
        string outputFilePath = Path.Combine(outputDirectoryPath, "Index.cshtml");


        string fileContent = File.ReadAllText(inputFilePath);




        string columnReplacement = @"
                {
                    'data': 'STT',
                    'title': 'STT',
                    'render': function (data, type, row, meta) {
                        return Index = meta.row + meta.settings._iDisplayStart + 1;
                    },
                    'orderable': false,
                    'visible': true,
                },";
        var itemProperty = listItemType;
        var coreEntityType = typeof(CoreEntity);

        var OtherProperties = itemProperty.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        foreach (PropertyInfo propertyInfo in OtherProperties)
        {
            if (propertyInfo.Name != "Id")
            {
                columnReplacement += $@"
                {{
                    'data': '{propertyInfo.Name}',
                    'title': '{propertyInfo.Name}',
                    'render': function (data, type, row) {{
                        return data ? data : ' ';
                    }},
                    'orderable': false,
                    'visible': true
                }},";
            }
        }



        columnReplacement += $@"{{
                    ""data"": '',
                    ""title"": ""Chức năng"",
                    ""render"": function (data, type, row) {{
                        var html = `<div class='button-container-in-grid button-container-${{row.Id}}'>`;
                        html += `<a href='#' title=""Sửa "" data-permission="""" onclick='sua{entityName}(${{row.Id}})'><i class=""fa fa-pencil"" aria-hidden=""true""></i></a>`
                        if (row.ModerationStatus != 0)
                            html += `<a href='#' title=""Duyệt "" data-permission="""" onclick='duyet{entityName}(${{row.Id}})'><i class=""fa fa-check"" aria-hidden=""true""></i></a>`
                        else
                            html += `<a href='#' title=""Hủy duyệt"" data-permission="""" onclick='huyDuyet{entityName}(${{row.Id}})'><i class=""fa fa-times"" aria-hidden=""true""></i></a>`

                        html += ` <a href='#' title=""Xóa"" data-permission="""" onclick='xoa{entityName}(${{row.Id}})'><i class=""fa fa-trash-o"" aria-hidden=""true""></i></a>`;
                        return html;
                    }},
                    ""orderable"": false,
                    ""width"": ""83px""
                }}";
        


        fileContent = fileContent.Replace("@columnsTemplate@", columnReplacement);
        fileContent = fileContent.Replace("@Entity@", entityName);

        File.WriteAllText(outputFilePath, fileContent);
    }

    private static void GenarateTemplateView(string entityName, Type listItemType)
    {
        var listPropertyInfo = listItemType.GetProperties();
        string outputDirectoryPath = $"Output/{entityName}";

        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string templatesFolder = Path.Combine(projectRoot, "Templates");
        string inputFilePath = Path.Combine(templatesFolder, "View.txt");
        string outputFilePath = Path.Combine(outputDirectoryPath, "View.cshtml");


        string fileContent = File.ReadAllText(inputFilePath);




        string columnReplacement = @"";

        var lstProperty = listItemType.GetProperties();
        foreach (PropertyInfo propertyInfo in lstProperty)
        {
            if (propertyInfo.Name != "Id")
            {


                columnReplacement += $@"<tr>
                    <td>
                        <label>{propertyInfo.Name}</label>
                    </td>
                    <td>
                        <p class=""itemtext"">@Model.{propertyInfo.Name}</p>
                    </td>

                </tr>";


            }
        }
        fileContent = fileContent.Replace("@columnsTemplate@", columnReplacement);
        fileContent = fileContent.Replace("@Entity@", entityName);

        File.WriteAllText(outputFilePath, fileContent);

    }
    private static void GenarateTemplateEdit(string entityName, Type listItemType)
    {
        var listPropertyInfo = listItemType.GetProperties();
        string outputDirectoryPath = $"Output/{entityName}";

        var entityNameRaw = entityName;

        if (entityName.StartsWith("L"))
            entityName = entityName.Substring(1);

        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string templatesFolder = Path.Combine(projectRoot, "Templates");
        string inputFilePath = Path.Combine(templatesFolder, "Edit.txt");
        string outputFilePath = Path.Combine(outputDirectoryPath, "Edit.cshtml");


        string fileContent = File.ReadAllText(inputFilePath);




        string columnReplacement = @"";

        var itemProperty = listItemType;

        var OtherProperties = itemProperty.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        foreach (PropertyInfo propertyInfo in OtherProperties)
        {
            if (propertyInfo.Name != "Id")
            {
                columnReplacement += $@"
                
                    <div class=""col-md-6"">
                        <div class=""mb-3"">
                            <label asp-for=""{propertyInfo.Name}"" class=""form-label"">{propertyInfo.Name}</label>
                            <input asp-for=""{propertyInfo.Name}"" class=""form-control "" placeholder=""{propertyInfo.Name}"" required />
                            <span asp-validation-for=""{propertyInfo.Name}"" class=""text-danger""></span>
                        </div>
                    </div>
                ";
            }
        }
        fileContent = fileContent.Replace("@columnsTemplate@", columnReplacement);
        fileContent = fileContent.Replace("@Entity@", entityNameRaw);
        File.WriteAllText(outputFilePath, fileContent);

    }


}


    public enum TypeError
    {

        Info = 1,
        Error = 2,
        Warning = 3,
    }
    public enum TypeRuning
    {

        Continute = 1,
        Stop = 2,
    }

