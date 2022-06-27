using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebApiNotaFiscal.Models;
using WebApiNotaFiscal;

namespace WebApiNotaFiscal.Controllers
{
    public class ConcessionariaException : ApplicationException
    {
        public string motivo { get;}
        public ConcessionariaException(string motivo)
        {
            this.motivo = motivo;
        }
    }
    public class SistemaConcessionariaTratamento
    {   
        Regex regexNome = new Regex(@"^([A-Z]?-?[a-záâíõãé]+\s?)+$");
        Regex regexTelefone = new Regex(@"^\(?[1-9]{2}\)?\s?(?:[2-8]|9[1-9])[0-9]{3}\-?\s?[0-9]{4}$");
        Regex cnpjRegex = new Regex(@"^\d{2}\.?\d{3}\.?\d{3}\/?\d{4}\-?\d{2}$");
        Regex cepRegex = new Regex(@"^\d{5}-?\d{3}$");
        Regex cpfRegex = new Regex(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$");
        Regex regexEmail = new Regex(@"^\w+?@[a-z]+?\.[a-z]+$");


        public void ConcessionariaTratamento(Concessionaria concessionaria){
            var cnpj = concessionaria.Cnpj;
            if(!cnpjRegex.IsMatch(cnpj)) throw new ConcessionariaException("cnpj invalido");
            if(!regexNome.IsMatch(concessionaria.Nome)) throw new ConcessionariaException("Nome invalido");
            if(!regexTelefone.IsMatch(concessionaria.Telefone)) throw new ConcessionariaException("Telefone invalido");
            if(concessionaria.DataFundacao == null) throw new ConcessionariaException("Data invalida");
        }

        public void EnderecoConcessionariaTratamento(EnderecoConcessionaria enderecoConcessionaria)
        {
            if(!cepRegex.IsMatch(enderecoConcessionaria.Cep)) throw new ConcessionariaException("Cep invalido");
            if(enderecoConcessionaria.Complemento == null) throw new ConcessionariaException("Complemento nao pode ser nulo complemento");
            if(enderecoConcessionaria.NumeroRua <= 0 ) throw new ConcessionariaException("Numero da rua nao pode ser menor ou igual a zero");
            if(!regexNome.IsMatch(enderecoConcessionaria.rua)) throw new ConcessionariaException("Nome de rua invalida");
            if(!regexNome.IsMatch(enderecoConcessionaria.bairro)) throw new ConcessionariaException("Nome de bairro invalido");
            if(!regexNome.IsMatch(enderecoConcessionaria.municipio)) throw new ConcessionariaException("Nome de municipio invalido");
            if(enderecoConcessionaria.uf.Length != 2) throw new ConcessionariaException("Uf de");
            
        }

        public void EnderecoClienteTratamento(Endereco enderecoCliente)
        {
            if(!cepRegex.IsMatch(enderecoCliente.Cep)) throw new ConcessionariaException("Cep invalido");
            if(enderecoCliente.Complemento == null) throw new ConcessionariaException("Complemento nao pode ser nulo complemento");
            if(enderecoCliente.NumeroRua <= 0 ) throw new ConcessionariaException("Numero da rua nao pode ser menor ou igual a zero");
            if(!regexNome.IsMatch(enderecoCliente.rua)) throw new ConcessionariaException("Nome de rua invalida");
            if(!regexNome.IsMatch(enderecoCliente.bairro)) throw new ConcessionariaException("Nome de bairro invalido");
            if(!regexNome.IsMatch(enderecoCliente.municipio)) throw new ConcessionariaException("Nome de municipio invalido");
            if(enderecoCliente.uf.Length != 2) throw new ConcessionariaException("Uf de");
            
        }

        public void ClienteTratamento(Cliente cliente) {
            if(!regexTelefone.IsMatch(cliente.TelefoneCliente)) throw new ConcessionariaException("telefone invalido");
            if(!regexNome.IsMatch(cliente.Nome)) throw new ConcessionariaException("Nome invalido");
            if(!cpfRegex.IsMatch(cliente.CPF)) throw new ConcessionariaException("Cpf invalido");
            if(!IsCpf(cliente.CPF)) throw new ConcessionariaException("CPF invalido");
            if(!regexEmail.IsMatch(cliente.EmailCliente)) throw new ConcessionariaException("Email invalido");
        }

        public void FuncionarioTratamento(Funcionario funcionario){
            if(!regexNome.IsMatch(funcionario.Nome)) throw new ConcessionariaException("Nome invalido");
            if(!cpfRegex.IsMatch(funcionario.CPF)) throw new ConcessionariaException("CPF invalido");
            if(!IsCpf(funcionario.CPF)) throw new ConcessionariaException("CPF invalido");
            if(funcionario.Salario <=1200) throw new ConcessionariaException("O salario é menor que o salario minimo (1200)");
        }

        public void ProdutoTratamento(Produto produto){
            List<string> coresDisponiveis = new List<string>{"azul","vermelho","prata","preto"};
            if(!coresDisponiveis.Contains(produto.cor)) throw new ConcessionariaException("Cor nao disponivel, as seguintes cores estao disponiveis: azul, veremlho, prata e preto");
            if(produto.Quilometragem<0) throw new ConcessionariaException("Um carro nao pode ter a sua quilometragem negativa");
            if(produto.Valor<=10000) throw new ConcessionariaException("Um carro que custa menos de 10k nao e vendido nessa concessionaria");
            if(!regexNome.IsMatch(produto.Fabricante)) throw new ConcessionariaException("Fabricante com caracteres invalidos");
        }       

        private bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;
            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}