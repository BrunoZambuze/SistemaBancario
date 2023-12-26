using System;
using SistemaBancario.Entidades.Exception;

namespace SistemaBancario.Entidades
{
    //Herança
    internal sealed class ContaEspecial : ContaCorrente
    {
        //Limite de 1000 reais negativos
        public ContaEspecial()
        {
        }

        //Reutilizar o construtor da ContaCorrente
        public ContaEspecial(string proprietario, double saldo, int numero, string tipo) : base(proprietario, saldo, numero, tipo)
        {
            Tipo = tipo;
        }

        //Sobreposição da função 'Sacar' para adicionar o limite
        public sealed override void Sacar(double valor)
        {
            if (Saldo - valor < -1000)
            {
                throw new DomainException("\n\nNão foi possível realizar o saque. O saldo da conta ultrapassou o limite!\n\n");
            }
            else
            {
                Saldo -= valor;
                Console.WriteLine("\n\nSaque realizado com sucesso!\n\n");
            }
        }
    }
}
