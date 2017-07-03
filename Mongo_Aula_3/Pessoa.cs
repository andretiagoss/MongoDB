using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Mongo_Aula_3
{
    public class Pessoa
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Idade { get; set; }

        public List<string> Treinamentos { get; set; }
    }

    [MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements] //ignora propriedades caso a mesma não exista mais na entidade
    public class Pessoa2
    {
        [BsonId] //Define o campo código como BsonId.
        public int Codigo { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonElement("Observacao")] //renomeia o nome
        [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfNull] //inora a propriedade caso seja nula
        public string Obs { get; set; }
        [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(BsonType.Double)] //converte para double
        public decimal Salario { get; set; }

        [MongoDB.Bson.Serialization.Attributes.BsonDateTimeOptions(Kind =DateTimeKind.Local)] //converte a data no formato local
        public DateTime DataInsercao { get; set; }

        [BsonIgnoreIfDefault] //ignora a propriedade quando a mesma conter valor default
        public int Contador { get; set; }

        [BsonDefaultValue("Casado")]
        public string EstadoCivil { get; set; }

    }
}
