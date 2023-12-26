using System;
using SistemaBancario.Entidades.Exception;

namespace SistemaBancario.Entidades
{
    internal class ContaCorrente
    {
        //Encapsulamento
        public string Proprietario { get; private set; }
        public double Saldo { get; protected set; }
        public int Numero { get; private set; }

        //Construtores
        public ContaCorrente()
        {
        }

        public ContaCorrente(string proprietario, double saldo, int numero)
        {
            Proprietario = proprietario;
            Saldo = saldo;
            Numero = numero;
        }

        //Função Depositar
        public void Depositar(double valor)
        {
            Saldo += valor;
        }

        //Função Sacar
        public virtual void Sacar(double valor)
        {
            if (Saldo - valor < 0)
            {
                throw new DomainException("\n\nNão foi possível realizar o saque. Saldo não deve ficar negativo!\n\n");
            }
            else
            {
                Saldo -= valor;
            }
        }

        //Função para imprimir os dados do cliente
        public override string ToString()
        {
            return $"\nNúmero da conta: {Numero}\nProprietário: {Proprietario}\nSaldo Atual: {Saldo:C}\n";
        }
    }
}
