using Domain.Models;
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
        Console.WriteLine("Nhập tên class:");
        string className = Console.ReadLine();

        // Tìm class dựa trên tên mà người dùng nhập
        Type entityType = Type.GetType(className);

        if (entityType != null)
        {
            // Gọi hàm tạo tự động nếu tìm thấy class
            GenarateTemplateMVC(entityType);
            Console.WriteLine("Tạo thành công Controller cho class: " + className);
        }
        else
        {
            Console.WriteLine("Không tìm thấy class: " + className);
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
            //GenarateTemplateEdit(entityName, listItemType);
            GenarateTemplateController(entityName, listItemType);
            GenarateTemplateIService(entityName, listItemType);
            GenarateTemplateService(entityName, listItemType);
            GenarateTemplateIRepository(entityName, listItemType);
            GenarateTemplateRepository(entityName, listItemType);


        Process.Start(new ProcessStartInfo
            {
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outputDirectoryPath),
                UseShellExecute = true
            });
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

            fileContent = fileContent.Replace("@Entity@", entityName);
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

        if (entityName.StartsWith("L"))
            entityName = entityName.Substring(1);

        string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string templatesFolder = Path.Combine(projectRoot, "Templates");
        string inputFilePath = Path.Combine(templatesFolder, "Repository.txt");
        string outputFilePath = Path.Combine(outputDirectoryPath, $"{entityName}Repository.cs");


        string fileContent = File.ReadAllText(inputFilePath);

        fileContent = fileContent.Replace("@Entity@", entityName);
        fileContent = fileContent.Replace("@EntityRaw@", entityNameRaw);

        File.WriteAllText(outputFilePath, fileContent);
    }

    private static void GenarateTemplateIndex(string entityName, Type listItemType)
    {
        var listPropertyInfo = listItemType.GetProperties();
        string outputDirectoryPath = $"Output/{entityName}";

        if (entityName.StartsWith("L"))
            entityName = entityName.Substring(1);

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

        var lstProperty = listItemType.GetProperties();

        foreach (PropertyInfo propertyInfo in lstProperty)
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



        columnReplacement += @"{
                    ""data"": '',
                    ""title"": ""Chức năng"",
                    ""render"": function (data, type, row) {
                        var html = `<div class='button-container-in-grid button-container-${row.ID}'>`;
                        html += `<a href='#' title=""Sửa "" data-permission="""" onclick='sua@Entity@(${row.ID},${row._ModerationStatus})'><i class=""fa fa-pencil"" aria-hidden=""true""></i></a>`
                        html += ` <a href='#' title=""Xóa"" data-permission="""" onclick='xoa@Entity@(${row.ID},${row._ModerationStatus})'><i class=""fa fa-trash-o"" aria-hidden=""true""></i></a>`;
                        return html
                    },
                    ""orderable"": false,
                    ""width"": ""83px""
                }";
        


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
    //private static void GenarateTemplateEdit(string entityName, Type listItemType)
    //{
    //    var listPropertyInfo = listItemType.GetProperties();
    //    string outputDirectoryPath = $"Output/{entityName}";

    //    var entityNameRaw = entityName;

    //    if (entityName.StartsWith("L"))
    //        entityName = entityName.Substring(1);

    //    string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
    //    string templatesFolder = Path.Combine(projectRoot, "Templates");
    //    string inputFilePath = Path.Combine(templatesFolder, "Edit.txt");
    //    string outputFilePath = Path.Combine(outputDirectoryPath, "Edit.cshtml");


    //    string fileContent = File.ReadAllText(inputFilePath);




    //    string columnReplacement = @"";
    //    string moreScript = @"";

    //    var lstProperty = listItemType.GetProperties();
    //    foreach (PropertyInfo propertyInfo in lstProperty)
    //    {
            


    //                template = $@" <div class=""col-md-6"">
    //        <div class=""mb-3"">
    //            @Html.LabelFor(model => model.{propertyInfo.Name}, ""{propertyInfo.Name}"", new {{ @class = ""form-label"" }})
    //            @Html.TextBoxFor(model => model.{propertyInfo.Name}, new
    //            {{
    //               @class = ""form-control"",
    //               placeholder = ""{propertyInfo.Name}"",
    //               {requiredAttr}
    //               {maxLengthAttr}
    //            }})
    //            @Html.ValidationMessageFor(model => model.{propertyInfo.Name}, ""Trường này là bắt buộc"", new {{ @class = ""invalid-feedback"" }})
    //            @Html.ValidationMessageFor(model => model.{propertyInfo.Name}, ""Dữ liệu hợp lệ"", new {{ @class = ""valid-feedback"" }})
    //        </div>
    //    </div>";
                

    //                templateScript = $@"var danhSachMacDinh{propertyInfo.Name} = [{{
    //        text: ""Gia tri mau"",
    //        id: 1
    //    }}];
    //    $(""#{propertyInfo.Name}"").registerSelect({{
    //        url: '/@ViewBag.AppName/@Entity@/GetPaged',
    //        placeholder: ""Chọn {{propertyInfo.Name}}"",
    //        queryData: {{
    //        }},
    //        initialSelection: danhSachMacDinh{propertyInfo.Name},
    //        prepareDataRequest: function (params) {{
    //            var parameter = {{Keyword: params.term,
    //                SearchIn: [""Title""],
    //                Paging: false,
    //                t_gridRequest: ""{{ 'skip': 0, 'page': 1, 'pageSize': 10 }}""
    //            }}
    //            return parameter;
    //        }},
    //        dataProcess: function (d) {{
    //            return {{
    //                results: d.Data.data.filter(item => item._ModerationStatus == 0).map(function (item) {{
    //                    return {{
    //                        id: item.ID,
    //                        text: item.Title
    //                    }};
    //                }})
    //            }};
    //        }},
    //    }});";
    //            }

    //            else if (propertyInfo.PropertyType == typeof(int))
    //            {
    //                template = $@"<div class=""col-md-6"">
    //                            <div class=""mb-3"">
    //                                @Html.LabelFor(model => model.{propertyInfo.Name}, ""{propertyInfo.Name}"", new {{ @class = ""form-label"" }})
    //                                @Html.TextBoxFor(model => model.{propertyInfo.Name}, new
    //                                {{
    //                                    @type = ""number"",
    //                                    @class = ""form-control"",
    //                                    placeholder = ""{propertyInfo.Name}"",
    //                                    {requiredAttr}
    //                                }})
    //                                @Html.ValidationMessageFor(model => model.{propertyInfo.Name}, ""Trường này là bắt buộc"", new {{ @class = ""invalid-feedback"" }})
    //                                @Html.ValidationMessageFor(model => model.{propertyInfo.Name}, ""Dữ liệu hợp lệ"", new {{ @class = ""valid-feedback"" }})

    //                            </div>
    //                        </div>";
    //            }
    //            else if (propertyInfo.PropertyType == typeof(bool))
    //            {
    //                template = $@"<div class=""col-12"">
    //                            <div class=""mb-3 form-check"">
    //                                @Html.TextBoxFor(model => model.{propertyInfo.Name}, new
    //                                {{
    //                                    @type = ""checkbox"",
    //                                    @class = ""form-check-input"",
    //                                    placeholder = ""{propertyInfo.Name}"",
    //                                    {requiredAttr}
    //                                }})
    //                                @Html.LabelFor(model => model.{propertyInfo.Name}, ""{propertyInfo.Name}"", new {{ @class = ""form-check-label"" }})
    //                                @Html.ValidationMessageFor(model => model.{propertyInfo.Name}, ""Trường này là bắt buộc"", new {{ @class = ""invalid-feedback"" }})
    //                                @Html.ValidationMessageFor(model => model.{propertyInfo.Name}, ""Dữ liệu hợp lệ"", new {{ @class = ""valid-feedback"" }})

    //                            </div>
    //                        </div>";
    //            }
    //            else if (propertyInfo.PropertyType == typeof(DateTime))
    //            {
    //                template = $@"<div class=""col-md-6""> <div class=""mb-3"">
    //    @Html.LabelFor(model => model.{propertyInfo.Name}, ""DateTime"", new {{ @class = ""form-label"" }})
    //    @Html.TextBox(""DateTime"", Model.{propertyInfo.Name} != DateTime.MinValue ? Model.{propertyInfo.Name}.ToString(""dd/MM/yyyy"") : DateTime.Now.ToString(""dd/MM/yyyy""), new
    //    {{
    //        @type = ""text"", 
    //        @class = ""form-control"",
    //        @placeholder = ""DateTime""
    //    }})
    //    @Html.ValidationMessageFor(model => model.{propertyInfo.Name}, ""Trường này là bắt buộc"", new {{ @class = ""invalid-feedback"" }})
    //    @Html.ValidationMessageFor(model => model.{propertyInfo.Name}, ""Dữ liệu hợp lệ"", new {{ @class = ""valid-feedback"" }})
    //</div></div>";

    //                templateScript = $@"
    //                        $('#{propertyInfo.Name}')
    //                            .datepicker()
    //                            .formatInputFriendly({{ type: 'date' }})
    //                            .on('change', function () {{
    //                                $('#{propertyInfo.Name}').data('datepicker').updateViewByData();
    //                            }});";
    //            }

    //            moreScript += templateScript;
    //            columnReplacement += template;

    //        }


    //    }
    //    fileContent = fileContent.Replace("@addingMoreScript@", moreScript);
    //    fileContent = fileContent.Replace("@columnsTemplate@", columnReplacement);
    //    fileContent = fileContent.Replace("@Entity@", entityName);
    //    fileContent = fileContent.Replace("@EntityRaw@", entityNameRaw);
    //    File.WriteAllText(outputFilePath, fileContent);
    //}









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

