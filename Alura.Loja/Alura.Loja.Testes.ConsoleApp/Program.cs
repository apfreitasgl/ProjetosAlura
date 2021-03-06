﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new LojaContext())
            {
                var produtos = contexto.Produtos.ToList();
                foreach (var p in produtos)
                    Console.WriteLine(p);

                Console.WriteLine("=================");
                foreach (var e in contexto.ChangeTracker.Entries())
                    Console.WriteLine(e.State);

                var p1 = produtos.Last();
                p1.Nome = "007 - O Espiao Que Me Amava";

                Console.WriteLine("=================");
                foreach (var e in contexto.ChangeTracker.Entries())
                    Console.WriteLine(e.State);

                //contexto.SaveChages();

                //Console.WriteLine("=================");
                //produtos = contexto.Produtos.ToList();
                //foreach (var p in produtos)
                //{
                //    Console.WriteLine(p);
                //}
            }
        }

        private static void AtualizarProduto()
        {
            GravarUsandoEntity();
            RecuperarProdutos();
            // atualiza o produto
            using (var contexto = new ProdutoDAOEntity())
            {
                Produto primeiro = contexto.Produtos().First();
                primeiro.Nome = "Cassino Royale - Editado";
                contexto.Adicionar(primeiro);
            }
            RecuperarProdutos();
        }

        private static void ExcluirProdutos()
        {
            using (var contexto = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = contexto.Produtos();
                foreach (var item in produtos)
                    contexto.Remover(item);
            }
        }

        private static void RecuperarProdutos()
        {
            using (var contexto = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = contexto.Produtos();
                Console.WriteLine($"foram encontrados {produtos.Count} produto(s)");
                foreach (var item in produtos)
                    Console.WriteLine(item.Nome);
            }
        }

        private static void GravarUsandoEntity()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.Preco = 19.89;

            using (var contexto = new ProdutoDAOEntity())
            {
                contexto.Adicionar(p);
            }
        }
        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.Preco = 19.89;

            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }
    }
}
