using System;
using System.Collections.Generic;
using WebApp_Desafio_BackEnd.DataAccess;
using WebApp_Desafio_BackEnd.Models;

namespace WebApp_Desafio_BackEnd.Business
{
    public class DepartamentosBLL
    {
        private DepartamentosDAL dal = new DepartamentosDAL();

        public IEnumerable<Departamento> ListarDepartamentos()
        {
            return dal.ListarDepartamentos();
        }

        public int AdicionarDepartamento(string descricao)
        {
            return dal.AdicionarDepartamento(descricao);
        }

        public void AtualizarDepartamento(int id, string descricao)
        {
            dal.AtualizarDepartamento(id, descricao);
        }

        public void ExcluirDepartamento(int id)
        {
            dal.ExcluirDepartamento(id);
        }

        public Departamento ObterDepartamentoPorId(int id)
        {
            return dal.ObterDepartamentoPorId(id);
        }
    }
}
