# AvaliacaoBari
Avaliação Banco Bari

# Configurações Necessárias #
 Visual Studio ou outra IDE de preferência
 .Net Core 3.1 ou mais
 Docker

# Para executar #
#Passo 1
 Abrir console na pasta pasta do projeto 
 Ao executar pela primeira vez rodar Docker com o comando: 
 docker-compose up -d
 O comando acima faz com que o ambiente esteja rodando o RabbitMq de forma correta
 Caso esteja executando o serviço pela segunda vez rodar no console o comando:
 docker container ps -a

# Passo 2
 Caso o status da imagem criada não esteja rodando, deve-se copiar o ID da imagem e rodar o seguinte comando
 docker start id_imagem

# Passo 3
 Abrir Solução no Visual Studio

# Passo 4
 Executar solução




