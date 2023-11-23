using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BancoDeDadosTI14T
{
    class DAO
    {
        public MySqlConnection conexao;
        public string dados;
        public string comando;
        public string resultado;
        public int i;
        public string msg;
        public int contador;
        public int[] codigo;//Vetor de código
        public string[] nome;//Vetor de nome
        public string[] telefone;//Vetor de telefone
        public string[] endereco;//Vetor de endereço
        public DateTime[] data;//Vetor de datas


        public DAO()
        {
            //Script para conexão do banco de dados
            conexao = new MySqlConnection("server=localhost;DataBase=turma14;Uid=root;Password=;Convert Zero DateTime=True");
            try
            {
                conexao.Open();//Tentando conectar ao BD
                Console.WriteLine("Conectado com sucesso!");
                
            }
            catch(Exception e)
            {
                Console.WriteLine("Algo deu errado!\n\n" + e);//Mostrar o erro em tela
                conexao.Close();//Fechar a conexão com o banco de dados
            }
        }//fim do método construtor

        //Método para inserir dados no BD
        public void Inserir(string nome, string telefone, string endereco, DateTime dtNascimento)
        {
            try
            {
                //Modificar a estrutura de data
                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = "@Date";
                parameter.MySqlDbType = MySqlDbType.Date;
                parameter.Value = dtNascimento.Year + "-" + dtNascimento.Month + "-" + dtNascimento.Day;
                //Preparo o código para inserção no banco
                dados = "('','" + nome + "','" + telefone + "','" + endereco + "','" + parameter.Value + "')";
                comando = "Insert into pessoa(codigo, nome, telefone, endereco, dataDeNascimento) values" + dados;
                //Executar o comando de inserção no banco de dados
                MySqlCommand sql = new MySqlCommand(comando, conexao);
                resultado = "" + sql.ExecuteNonQuery();//Executa o insert no BD
                Console.WriteLine(resultado + " Linhas Afetadas");
            }
            catch(Exception e)
            {
                Console.WriteLine("Algo deu errado!\n\n" + e);
                Console.ReadLine();//Manter o programa aberto
            }
        }//fim do método inserir

        public void PreencherVetor()
        {
            string query = "select * from pessoa";//Coletar os dados do BD

            //Instanciar
            codigo = new int[100];
            nome     = new string[100];
            telefone = new string[100];
            endereco = new string[100];
            data     = new DateTime[100];

            //Preencher com valores iniciais
            for(i=0; i < 100; i++)
            {
                codigo[i] = 0;
                nome[i] = "";
                telefone[i] = "";
                endereco[i] = "";
                data[i] = new DateTime();
            }//fim do for

            //Criando o comando para consultar no BD
            MySqlCommand coletar = new MySqlCommand(query, conexao);
            //Leitura dos dados do banco
            MySqlDataReader leitura = coletar.ExecuteReader();

            i = 0;
            contador = 0;
            while (leitura.Read())
            {
                codigo[i]   = Convert.ToInt32(leitura["codigo"]);
                nome[i]     = leitura["nome"]     + "";
                telefone[i] = leitura["telefone"] + "";
                endereco[i] = leitura["endereco"] + "";
                data[i] = Convert.ToDateTime(leitura["dataDeNascimento"]);
                i++;
                contador++;
            }//Fim do while

            //Fechar a leitura de dados no banco
            leitura.Close();
        }//fim do método de preenchimento do vetor

        //Método que consulta TODOS OS DADOS no banco de dados
        public string ConsultarTudo()
        {
            //Preencher os vetores
            PreencherVetor();
            msg = "";
            for(i = 0; i < contador; i++)
            {
                msg += "Código: "               + codigo[i]   +
                       ", Nome: "               + nome[i]     +
                       ", Telefone: "           + telefone[i] +
                       ", Endereço: "           + endereco[i] +
                       ", Data de Nascimento: " + data[i]     +
                       "\n\n";
            }//fim do for

            return msg;
        }//fim do método consultarTudo

        public string ConsultarTudo(int cod)
        {
            PreencherVetor();
            for(i=0; i < contador; i++)
            {
                if(codigo[i] == cod)
                {
                    msg = "Código: " + codigo[i] +
                       ", Nome: " + nome[i] +
                       ", Telefone: " + telefone[i] +
                       ", Endereço: " + endereco[i] +
                       ", Data de Nascimento: " + data[i] +
                       "\n\n";
                    return msg;
                }
            }//fim do for
            return "Código informado não encontrado!";
        }//fim do consultar

        public string Atualizar(int codigo, string campo, string novoDado)
        {
            try
            {
                string query = "update pessoa set " + campo + " = '" + novoDado + "' where codigo = '" + codigo + "'";
                //Executar o comando
                MySqlCommand sql = new MySqlCommand(query, conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                return resultado + "Linha Afetada";
            }
            catch(Exception e)
            {
                return "Algo deu errado!\n\n" + e;
            }
        }//fim do método Atualizar

        public string Deletar(int codigo)
        {
            try
            {
                string query = "delete from pessoa where codigo = '" + codigo + "'";
                //Preparar o comando
                MySqlCommand sql = new MySqlCommand(query, conexao);
                string resultado = "" + sql.ExecuteNonQuery();
                //Mostrar a mensagem em tela
                return resultado + "Linha Afetada";
            }
            catch(Exception e)
            {
                return "Algo deu errado!\n\n" + e;
            }
        }//fim do deletar
    }//fim da classe
}//fim do projeto
