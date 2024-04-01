using Microsoft.AspNetCore.Mvc;
using System;
using WebApp_Desafio_FrontEnd.ApiClients.Desafio_API;
using WebApp_Desafio_FrontEnd.ViewModels;

namespace WebApp_Desafio_FrontEnd.Controllers
{
    public class DepartamentosController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public IActionResult Listar()
        {
            // Busca de dados está na Action Datatable()
            return View();
        }

        [HttpGet]
        public IActionResult Datatable()
        {
            try
            {
                var departamentosApiClient = new DepartamentosApiClient();
                var lstDepartamentos = departamentosApiClient.DepartamentosListar();

                var dataTableVM = new DataTableAjaxViewModel()
                {
                    length = lstDepartamentos.Count,
                    data = lstDepartamentos
                };

                return Ok(dataTableVM);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(DepartamentoViewModel departamento)
        {
            try
            {
                var departamentosApiClient = new DepartamentosApiClient();
                var idDepartamento = departamentosApiClient.DepartamentoGravar(departamento);

                return Ok(idDepartamento);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }


        [HttpGet]
        public IActionResult Cadastrar()
        {
            var departamentoVM = new DepartamentoViewModel()
            {
                Descricao = ""
            };
            ViewData["Title"] = "Cadastrar Novo departamento";

            try
            {
                var departamentosApiClient = new DepartamentosApiClient();
                ViewData["ListaDepartamentos"] = departamentosApiClient.DepartamentosListar();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
            }

            return View("Cadastrar", departamentoVM); // Chama a view de cadastrar
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            try
            {
                var departamentosApiClient = new DepartamentosApiClient();
                var departamento = departamentosApiClient.DepartamentoObter(id);

                if (departamento == null)
                {
                    return NotFound();
                }

                var departamentoVM = new DepartamentoViewModel()
                {
                    ID = departamento.ID,
                    Descricao = departamento.Descricao
                };

                ViewData["Title"] = "Editar Departamento";
                return View("Editar", departamentoVM);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpPut]
        public IActionResult Atualizar(int id, string descricao)
        {
            try
            {
                var departamentosApiClient = new DepartamentosApiClient();
                var success = departamentosApiClient.DepartamentoAtualizar(id, descricao);

                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            try
            {
                var departamentosApiClient = new DepartamentosApiClient();
                var success = departamentosApiClient.DepartamentoExcluir(id);

                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }
    }
}
