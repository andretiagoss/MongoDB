using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo_Aula_3
{
    class Program
    {
        static void Main(string[] args)
        {
            //DocumentBson();

            //Insert1().GetAwaiter().GetResult();

            //Insert2().GetAwaiter().GetResult();

            Insert3().GetAwaiter().GetResult();

            Find().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Método com exemplo de criação e exibição do documento no console.
        /// </summary>
        static void DocumentBson()
        {
            var doc = new BsonDocument();

            doc.Add("nome", "joao");
            doc.Add("idade", 20);

            var array = new BsonArray();
            array.Add(new BsonDocument("treinamento", "c#"));
            array.Add(new BsonDocument("treinamento", "mongo"));

            doc.Add("treinamentos", array);
            //Console.WriteLine(doc);

            //procedimento para capturar elemento.
            var arrayCapturado = new BsonElement();
            doc.TryGetElement("treinamentos", out arrayCapturado);
            Console.WriteLine(arrayCapturado);

            Console.ReadKey();
        }

        /// <summary>
        /// Método que retorna um documento criado manualmente.
        /// </summary>
        /// <returns></returns>
        static BsonDocument BsonDocumentBson()
        {
            //estancia do BsonDocument
            var doc = new BsonDocument();

            //Adicinando os dados (chave e valor) do documento
            doc.Add("nome", "joao");
            doc.Add("idade", 20);

            //criação de um array para adicionar no documento
            var array = new BsonArray();
            array.Add(new BsonDocument("treinamento", "c#"));
            array.Add(new BsonDocument("treinamento", "mongo"));

            //adicionando o array criado no documento como propriedade (campo) "treinamentos".
            doc.Add("treinamentos", array);

            //procedimento para capturar elemento.
            var arrayCapturado = new BsonElement();
            doc.TryGetElement("treinamentos", out arrayCapturado);

            return doc;
        }

        /// <summary>
        /// Método para inclusão de documentos com base no documento criado no métod BsonDocumentBson.
        /// </summary>
        /// <returns></returns>
        static async Task Insert1()
        {
            var stringConexao = "mongodb://localhost:27017";
            var client = new MongoClient(stringConexao);

            var db = client.GetDatabase("treinamentoMongo");
            var coll = db.GetCollection<BsonDocument>("Pessoa");

            var doc = BsonDocumentBson();
            await coll.InsertOneAsync(doc);
        }

        /// <summary>
        /// Método para inclusão de documento com base na classe Pessoa criado.
        /// </summary>
        /// <returns></returns>
        static async Task Insert2()
        {
            var stringConexao = "mongodb://localhost:27017";
            var client = new MongoClient(stringConexao);

            var db = client.GetDatabase("treinamentoMongo");
            var coll = db.GetCollection<Pessoa>("Pessoa");

            var pessoa = new Pessoa()
            {
                Id = 1,
                Nome = "Maria",
                Idade = 18,
                Treinamentos = new List<string>() { "c#", "MongoDB" }
            };

            await coll.InsertOneAsync(pessoa);
        }

        static async Task Insert3()
        {
            var stringConexao = "mongodb://localhost:27017";
            var client = new MongoClient(stringConexao);

            var db = client.GetDatabase("treinamentoMongo");
            var coll = db.GetCollection<Pessoa2>("Pessoa");

            var pessoa = new Pessoa2()
            {
                Codigo = new Random().Next(1, 1000),
                Salario = 1000,
                DataInsercao = DateTime.Now
            };

            await coll.InsertOneAsync(pessoa);
        }

        /// <summary>
        /// Metodo para mostrar a data convertida.
        /// </summary>
        /// <returns></returns>
        static async Task Find()
        {
            var stringConexao = "mongodb://localhost:27017";
            var client = new MongoClient(stringConexao);

            var db = client.GetDatabase("treinamentoMongo");
            var coll = db.GetCollection<Pessoa2>("Pessoa");

            var pessoa = await coll.Find(f => true).ToListAsync();            
        }
    }
}
