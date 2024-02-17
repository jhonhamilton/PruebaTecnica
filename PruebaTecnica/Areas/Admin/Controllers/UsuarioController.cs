using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PruebaTecnica.Models.Model;
using PruebaTecnica.Models.ViewModel;
using PruebaTecnica.Servicio.Interfaces;
using System.Data;
using System.Reflection;

namespace PruebaTecnica.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServices _servicio;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UsuarioController(IUsuarioServices servicio, IWebHostEnvironment hostingEnvironment)
        {
            _servicio = servicio;
            _hostingEnvironment=hostingEnvironment;
        }
        [Route("Admin")]
        [Route("Admin/Usuario")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
        [HttpGet]
        [Route("Admin/Usuario/GetUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var _users = await _servicio.Execute("S");
            //var _users = await _servicio.GetUsuarios();
            var usuarios = (from u in _users
                            select new UsuarioViewModel
                            {
                                Nombre = u.Nombre,
                                Fecha = u.FechaNacimiento.ToString("dd/MM/yyy"),
                                Sexo = (u.Sexo == "H" ? "Hombre" : (u.Sexo == "M" ? "Mujer" : "")),
                                UsuarioId = u.UsuarioId
                            }).ToList();
            var dataTable = ToDataTable<UsuarioViewModel>(usuarios);
            dataTable.TableName = "PruebaTecnica";
            return Json(new { data = usuarios, FileContent = CreateExcelAsync(dataTable), NameFile = "PruebaTecnica.xlsx" });
        }
        private async Task<FileContentResult> CreateExcelAsync(DataTable dataTable)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using var pack = new ExcelPackage();
            var ws = pack.Workbook.Worksheets.Add(dataTable.TableName);
            ws.Cells["A1"].LoadFromDataTable(dataTable, true);
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                ws.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#7ecbd3"));
            }
            if (!Directory.Exists(_hostingEnvironment.WebRootPath + "\\ArchivosExcel"))
            {
                Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\ArchivosExcel");
            }
            await pack.SaveAsAsync(new FileInfo(_hostingEnvironment.WebRootPath + "\\ArchivosExcel\\" + dataTable.TableName + ".xlsx"));
            FileContentResult byteContent = new FileContentResult(System.IO.File.ReadAllBytes(_hostingEnvironment.WebRootPath + "\\ArchivosExcel\\" + dataTable.TableName + ".xlsx"), "application/vnd.ms-excel")
            {
                FileDownloadName = dataTable.TableName + ".xlsx"
            };
            System.IO.File.Delete(_hostingEnvironment.WebRootPath + "\\ArchivosExcel\\" + dataTable.TableName + ".xlsx");
            return byteContent;
        }
        private static DataTable ToDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();
            DataTable dataTable = new();

            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length - 1 + 1];

                for (int i = 0; i <= properties.Length - 1; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        [HttpPost]
        [Route("Admin/Usuario/Create")]
        public async Task<JsonResult> Create(UsuarioViewModel user)
        {
            Usuario _user = new()
            {
                UsuarioId = user.UsuarioId,
                Nombre = user.Nombre,
                FechaNacimiento = Convert.ToDateTime(user.Fecha),
                Sexo = user.Sexo
            };
            string _mensaje = "Registro Guardado Correctamente";
            if (await _servicio.ExisteUsuario(_user))
            {
                var _users = await _servicio.Execute("U", _user);
                _user = _users.First();
                //_user = await _servicio.UpdateUsuario(_user.UsuarioId, _user);
                _mensaje = "Registro Editado Correctamente";
            }
            else
            {
                //_user = await _servicio.CreteUsuario(_user);
                var _users = await _servicio.Execute("I", _user);
                _user = _users.First();
            }
            return Json(new { success = _user.UsuarioId > 0, user = _user, message = _mensaje });
        }
        [HttpPut]
        [Route("Admin/Usuario/Delete")]
        public async Task<IActionResult> Delete(int usuarioId)
        {
            var usuario = await _servicio.GetUsuario(usuarioId);
            var _users = await _servicio.Execute("D", usuario);
            Usuario? _user = _users.FirstOrDefault();
            //Usuario _user = await _servicio.DeleteUsuario(usuarioId);
            return Json(new { success = _user?.UsuarioId > 0, user = _user, message = "Registro Eliminado Exitosamente" });
        }
    }
}
