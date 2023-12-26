using System;
using System.Globalization;
using System.Collections.Generic;
using SistemaBancario.Entidades;
using SistemaBancario.Entidades.Exception;
namespace SistemaBancario
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                List<ContaCorrente> listaConta = new List<ContaCorrente>();

                //Adicionar os dados da conta cadastrada em uma lista

                Console.WriteLine("Por gentileza preencha os campos com seus dados:");
                Console.Write("Número da conta: ");
                int numeroConta = int.Parse(Console.ReadLine());
                Console.Write("Nome: ");
                string nome = Console.ReadLine();
                Console.Write("Saldo atual: R$ ");
                double saldo = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.Write("Conta Corrente ou Especial (C/E): ");
                char tipoConta = char.Parse(Console.ReadLine());

                ContaCorrente contaCorrente;

                if (tipoConta == 'c' || tipoConta == 'C')
                {
                    contaCorrente = new ContaCorrente(nome, saldo, numeroConta);
                    listaConta.Add(contaCorrente);
                }
                else if (tipoConta == 'e' || tipoConta == 'E')
                {
                    //Polimorfismo
                    contaCorrente = new ContaEspecial(nome, saldo, numeroConta);
                    listaConta.Add(contaCorrente);
                }
                Console.Clear();

                //Interface interativa com o usuário
                int resp;
                do
                {
                    Console.WriteLine("---------Banco do Brasil---------");
                    foreach (ContaCorrente conta in listaConta)
                    {
                        Console.WriteLine(conta);
                    }
                    Console.WriteLine("[1]Sacar");
                    Console.WriteLine("[2]Depositar");
                    Console.WriteLine("[3]Sair");
                    Console.Write("\nEntre com o serviço desejado: ");
                    resp = int.Parse(Console.ReadLine());
                    switch (resp)
                    {
                        case 1:
                            Console.WriteLine("\n*** Serviço escolhido: Sacar ***\n");
                            Console.Write("Digite o valor que deseja sacar: ");
                            double valorSaque = double.Parse(Console.ReadLine());

                            //Verificar se o número da conta cadastrada pertence à lista, assim podemos usar a função de sacar
                            ContaCorrente contaC = listaConta.Find(x => x.Numero == numeroConta);
                            if (contaC != null)
                            {
                                contaC.Sacar(valorSaque);
                            }
                            break;
                        case 2:
                            Console.WriteLine("\n**Serviço escolhido: Depositar ***\n");
                            Console.Write("Digite o valor que deseja depositar: ");

                            //Verificar se o número da conta cadastrada pertence à lista, assim podemos usar a função de depositar
                            double valorDeposito = double.Parse(Console.ReadLine());
                            ContaCorrente contaD = listaConta.Find(x => x.Numero == numeroConta);
                            if (contaD != null)
                            {
                                contaD.Depositar(valorDeposito);
                            }
                            break;
                    }
                } while (resp != 3);
                Console.WriteLine("\n\n***OBGRIADO POR ACESSAR NOSSO SERVIÇO***\n\n");
            }

            //Exceções que lançam um erro caso algo saia do padrão
            catch (DomainException error)
            {
                Console.WriteLine($"Error: {error.Message}");
            }
            catch (FormatException error)
            {
                Console.WriteLine("Error: Dado não digitado no formato correto!");
            }
        }
    }
}