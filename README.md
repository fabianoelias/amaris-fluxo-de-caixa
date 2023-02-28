# AMARIS-FLUXO-DE-CAIXA

Aplicativo para controle de fluxo de caixa. Permite que o usuário faça a gestão de caixa registrando lançamentos (débito e crédito) e exibe um consolidado dos lançamentos por caixa. Consiste de uma aplicação que pode ser acessada via Web ou Mobile. A Aplicação também disponibiliza uma API baseada em Swagger.

# TECNOLOGIAS

* DotNet Core 7.0
* Entity Framework
* Swagger
* Docker

# DESIGN PATTERNS

# Code First

Solução construída utilizando a estratégia Code First. Você terá controle total sobre o código. Você simplesmente define e cria entidades POCO e deixa a EF gerar o banco de dados correspondente para você. A desvantagem é que se você alterar algo em seu banco de dados manualmente, provavelmente os perderá porque seu código define o banco de dados. É fácil de modificar e manter, pois não haverá código gerado automaticamente. Modelo ilustrado abaixo:

![image](https://user-images.githubusercontent.com/11980542/221722859-d75034b5-0883-4d86-98d3-3ca166cec881.png)

Figura 1 - Code First

# ASP.NET Web Api

É uma estrutura usada para criar serviços HTTP e é uma plataforma ideal para criar aplicativos RESTful no .NET Framework.  Nível de Maturidade 2, nesse nível de maturidade a mesma ideia deve ser utilizada com os verbos HTTP, eles devem ser suficientes para um CRUD (Create, Read, Update, Delete). Para cada ação de um recurso da API são aplicados verbos diferentes.

![image](https://user-images.githubusercontent.com/11980542/221858483-fcadad35-418d-496e-b0b3-6502ec818aeb.png)

Figura 2 - APS.NET Web API Nível 2 de Maturidade

# ASP.NET Core MVC

É uma maneira baseada em padrões para construir sites dinâmicos que permitem uma separação limpa de interesses e que lhe dá controle total sobre marcação ou HTML.

Usaremos o ASP.NET Core para criar APIs REST (Web API) e como nosso framework front-end (MVC) para gerar páginas ou visualizações (também conhecidas como User Interface). Por outro lado, usaremos o Entity Framework Core como nosso mecanismo de acesso aos dados para trabalhar com os dados do banco de dados. A figura abaixo ilustra como cada tecnologia interage entre si.

![image](https://user-images.githubusercontent.com/11980542/221722884-7080686a-9a8e-4efe-8958-035876117848.png)

Figura 3 - Processo

# SOLID

SOLID é um acrônimo para cinco postulados de design de código em POO (Programação Orientada a Objeto), utilizados para facilitar a compreensão, o desenvolvimento e a manutenção de software.

A aplicação foi desenvolvida respeitando os 5 princípios do SOLID:

* Single Responsability Principle (Princípio da Responsabilidade Única);

   Esse princípio nos diz que cada classe dentro de um projeto deve se especializar em um único assunto e possuir uma única responsabilidade, tendo uma única tarefa ou ação para executar.
* Open/Closed Principle (Princípio Aberto/Fechado);

  Esse princípio nos diz que, ao criar um objeto ou entidade, devemos prepará-lo para que possa ser implementado por outro futuramente. Assim, não será necessário modificar o objeto pai.
* Liskov Substitution Principle (Princípio da substituição de Liskov);

  Este princípio nos garante o desacoplamento dos nossos objetos, possibilitando uma melhor manutenção e atualização do nosso código quando necessário.
* Interface Segregation Principle (Princípio da Segregação de Interface);

  Esse princípio mostra que devemos criar interfaces mais específicas para nossos objetos, ao invés de uma classe mais genérica para todos do mesmo tipo.
* Dependency Inversion Principle (Princípio da Inversão de Dependência).

  Com a Inversão de Dependência, conseguimos desacoplar nossas classes de bibliotecas específicas e fazer com que outras ferramentas possam ser utilizadas no lugar desta primeira. Assim, as classes utilizarão abstrações de interfaces ao invés de outras classes ou de instâncias objetos. 
  
 ![image](https://user-images.githubusercontent.com/11980542/221860812-64902ba1-690f-4071-8ac9-f203cad48739.png)

Figura 4 - Interface IRepository

# EXECUTANDO O PROJETO

# Requisitos:

* Visual Studio
* DotNet Core 7.0
* Docker for Desktop

# Modo 1 - Visual Studio
* Clone o repositório para um diretório local
* Abra a Solução com o Visual Studio
* Execute a Solução (F5)

# Modo 2 - Docker Desktop
* Clone o repositório para um diretório local
* Abra a Solução com o Visual Studio
* Abra o Terminal
* Executar docker-compose up --build
