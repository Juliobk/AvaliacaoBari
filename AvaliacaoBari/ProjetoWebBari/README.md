# AvaliacaoBari  
Avaliação Banco Bari  

#Sobre o Projeto
O projeto consiste em um serviço que envia e recebe mensagens através do sistema de mensagens "rabbitmq"  
O projeto deve ser rodado através do docker, que irá criar três containers, sendo um para o rabbitmq e duas instâncias do serviço do projeto.

# Configurações Necessárias    
.Net Core 3.1  
Docker  

# Para executar   
Abrir console na pasta pasta da solução e rodar o comando abaixo para criar a imagem do projeto   
docker build -f Sender/Dockerfile -t imagemprojetobari .  
Em seguida rodar o comando abaixo:  
docker-compose up -d  





