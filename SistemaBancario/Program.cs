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
                int numeroConta = 0;

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
                    Console.WriteLine("[1]Cadastrar");
                    Console.WriteLine("[2]Sacar");
                    Console.WriteLine("[3]Depositar");
                    Console.WriteLine("[4]Transferir");
                    Console.WriteLine("[5]Sair");
                    Console.Write("\nEntre com o serviço desejado: ");
                    resp = int.Parse(Console.ReadLine());
                    switch (resp)
                    {
                        case 1:
                            //Adicionar os dados da conta cadastrada em uma lista

                            Console.WriteLine("\nPor gentileza preencha os campos com seus dados:");
                            Console.Write("Número da conta: ");
                            numeroConta = int.Parse(Console.ReadLine());
                            Console.Write("Nome: ");
                            string nome = Console.ReadLine();
                            Console.Write("Saldo atual: R$ ");
                            double saldo = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                            Console.Write("Conta Corrente ou Especial (C/E): ");
                            char tipoConta = char.Parse(Console.ReadLine());

                            ContaCorrente contaCorrente;

                            if (tipoConta == 'c' || tipoConta == 'C')
                            {
                                contaCorrente = new ContaCorrente(nome, saldo, numeroConta, "Corrente");
                                listaConta.Add(contaCorrente);
                            }
                            else if (tipoConta == 'e' || tipoConta == 'E')
                            {
                                //Polimorfismo
                                contaCorrente = new ContaEspecial(nome, saldo, numeroConta, "Especial");
                                listaConta.Add(contaCorrente);
                            }
                            break;
                        case 2:
                            //Sacar
                            Console.WriteLine("\n*** Serviço escolhido: Sacar ***\n");
                            Console.Write("Digite o numero da conta que deseja sacar: ");
                            int numContaSaq = int.Parse(Console.ReadLine());
                            Console.Write("Digite o valor que deseja sacar: ");
                            double valorSaque = double.Parse(Console.ReadLine());

                            //Verificar se o número da conta cadastrada pertence à lista, assim podemos usar a função de sacar
                            ContaCorrente contaC = listaConta.Find(x => x.Numero == numContaSaq);
                            if (contaC != null)
                            {
                                contaC.Sacar(valorSaque);
                            }
                            else
                            {
                                //Exceção lançada caso o número digitado não esteja na lista
                                throw new DomainException($"Número digitado ({numContaSaq}) não está cadastrado!");
                            }
                            break;
                        case 3:
                            //Depositar
                            Console.WriteLine("\n**Serviço escolhido: Depositar ***\n");
                            Console.Write("Digite o número da conta que deseja depositar: ");
                            int numContaDep = int.Parse(Console.ReadLine());
                            Console.Write("Digite o valor que deseja depositar: ");

                            //Verificar se o número da conta cadastrada pertence à lista, assim podemos usar a função de depositar
                            double valorDeposito = double.Parse(Console.ReadLine());
                            ContaCorrente contaD = listaConta.Find(x => x.Numero == numContaDep);
                            if (contaD != null)
                            {
                                contaD.Depositar(valorDeposito);
                            }
                            else
                            {
                                //Exceção lançada caso o número digitado não esteja na lista
                                throw new DomainException($"Número digitado ({numContaDep}) não existe!");
                            }
                            break;
                        case 4:
                            //Transferir
                            Console.WriteLine("\n**Serviço escolhido: Transferência ***\n");
                            Console.Write("Digite o número da sua conta atual: ");
                            int contaA = int.Parse(Console.ReadLine());
                            Console.Write("Digite o número da conta que deseja realizar a transferência: ");
                            int contaB = int.Parse(Console.ReadLine());
                            Console.Write("Digite o valor que deseja transferir: ");
                            double valorTrasnf = double.Parse(Console.ReadLine());

                            //Verificar se o número da conta está cadastrado
                            ContaCorrente contaTransfA = listaConta.Find(x => x.Numero == contaA);
                            ContaCorrente contaTransfB = listaConta.Find(x => x.Numero == contaB);
                            if (contaTransfA != null && contaTransfB != null)
                            {
                                contaTransfA.Sacar(valorTrasnf);
                                contaTransfB.Depositar(valorTrasnf);
                            }
                            else
                            {
                                throw new DomainException("O número da conta digitado é inválido!");
                            }
                            break;
                    }
                } while (resp != 5);
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