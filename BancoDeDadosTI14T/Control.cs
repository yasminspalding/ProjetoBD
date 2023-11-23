using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoDeDadosTI14T
{
    class Control
    {
        
        DAO conexao;//Criando a variável conexao
        public int opcao;
        public DateTime dtNascimento;
        public Control()
        {
            conexao = new DAO();//Instanciando a variável conexao
            dtNascimento = new DateTime();//00/00/0000 00:00:00
        }//fim do construtor

        public void Menu()
        {
            Console.WriteLine("Escolha uma das opções abaixo: \n\n" +
                              "1. Cadastrar\n" +
                              "2. Consultar Tudo\n" +
                              "3. Consultar por Código\n" +
                              "4. Atualizar\n" +
                              "5. Excluir\n"   +
                              "0. Sair");
            opcao = Convert.ToInt32(Console.ReadLine());
        }//fim do menu

        public void Executar()
        {
            Menu();//Chamar o menu com todos os dados
            do
            {
                switch (opcao)
                {
                    case 1:
                        //Coletando dados
                        Console.WriteLine("Informe seu nome: ");
                        string nome = Console.ReadLine();
                        Console.WriteLine("Informe seu telefone: ");
                        string telefone = Console.ReadLine();
                        Console.WriteLine("Informe seu endereço: ");
                        string endereco = Console.ReadLine();
                        Console.WriteLine("Informe sua data de nascimento: ");
                        dtNascimento = Convert.ToDateTime(Console.ReadLine());
                        //Utilizando esses dados no método inserir
                        conexao.Inserir(nome, telefone, endereco, dtNascimento);
                        break;
                    case 2:
                        Console.WriteLine(conexao.ConsultarTudo());
                        break;
                    case 3:
                        //Coletando o código que será pesquisado
                        Console.WriteLine("Informe o código do cliente que deseja consultar: ");
                        int codigo = Convert.ToInt32(Console.ReadLine());
                        //Mostrando o resultado em tela
                        Console.WriteLine(conexao.ConsultarTudo(codigo));
                        break;
                    case 4:
                        //Solicitar os campos que serão atualizados
                        Console.WriteLine("Informe o campo que deseja atualizar: ");
                        string campo = Console.ReadLine();
                        Console.WriteLine("Informe o novo dado para esse campo: ");
                        string novoDado = Console.ReadLine();
                        Console.WriteLine("Informe o código do usuário que deseja atualizar: ");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        //utilizar os dados acima no método atualizar
                        Console.WriteLine(conexao.Atualizar(codigo, campo, novoDado));
                        break;
                    case 5:
                        //Solicitar o código que será apagado
                        Console.WriteLine("Informe o código que deseja apagar");
                        codigo = Convert.ToInt32(Console.ReadLine());
                        //Mostrar o resultado em tela
                        Console.WriteLine(conexao.Deletar(codigo));
                        break;
                    default:
                        Console.WriteLine("Código informado não é válido!");
                        break;
                }//fim do switch
            } while (opcao != 0);
        }//fim do executar
    }//fim da classe
}//fim do projeto
