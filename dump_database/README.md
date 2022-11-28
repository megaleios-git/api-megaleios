to_host = IP DO SERVER DO BANCO

NECESSARIO mongodbtools

ABRIR CMD NA PASTA "Megaleios" rodar o comando abaixo para restaurar tabelas do banco
para uso das apis de consulta de Cidade, Estado,Busca por CEP, Bancos da iugu etc

mongorestore --host <to_host> --archive

OS DADOS IMPORTADOS SERÃO CONSUMIDOS ATRAVES DA API CONTIDA NO PROJETO ASP.NET CORE 1.1 Megaleios.WebApi
