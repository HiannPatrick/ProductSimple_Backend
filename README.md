# ProductSimple_Backend
-> Configurações:
- Faça o clone do projeto em: https://github.com/HiannPatrick/ProductSimple_Backend.git;
- Certifique-se de ter o Docker instalado em seu computador: https://docs.docker.com/desktop/;
- Abra o terminal;
- Utilize o comando "cd", e navegue até o diretório onde o arquivo "docker-compose.yaml" está;
- Se é a primeira vez que irá criar um container do projeto, execute: "docker-compose up -d";
- Se não é a primeira vez, execute antes: "docker-compose build" e depois "docker-compose up -d";
- Quando o container for criado e iniciado, abra o navegador com a URL: "http://localhost:8080/swagger/index.html";

-> Considerações:
  - Ao executar o container, o banco de dados e suas tabelas serão criadas automaticamente;
  - Para fins de demonstração, a tabela de usuário, será populada automaticamente com os dados:
      Usuario admin:
        - email: admin@admin.com
        - senha: $2a$11$oyUC8mS2uskAM9T1NKJ9IOVktAx4aKdETVW0bjp2NXQ3zMYsSoKK2

       Usuario comum:
        - email: user@user.com
        - senha: $2a$11$3irNpzFpedrYbuPLzlMGoudO1TqMoVuE7YLJICNJgL9R0conpGSBe
  - O usuário "admin", tem permissão para acessar qualquer api no sistema;
  - O usuário "user", só possui permissões de leitura (verbos "Get");

-> Testes:
 - No swagger, clique na API referente ao "Login", e digite o email e senha do perfil que voce quer testar: admin ou user;
 - O sistema deve retornar o token de aecsso;
 - Copie o token;
 - Clique no botão "Authorize" no canto superior direito da tela;
 - Na caixa de texto que aprecerá, cole o token e cliqe em "Ok";
 - É só acessar as APIs normalmente;
 - Se foi feito login com um usuário do tipo "user", ele não terá permissão para criar, editar e excluir quaisquer dados.

   -> Desafio
   O desafio consiste na implementação de um sistema back-end e integração 
com o banco de dados que gerencia produtos, desta forma, deve ser entregue 
um CRUD de produtos, utilizando os cinco endpoints no padrão REST. No 
back-end deve ser utilizado PHP com Laravel, ou linguagem de sua 
preferência o, e banco de dados MySQL 8.0. 
O sistema deve ser acessado com autenticação por token (JWT, Oauth2, 
Laravel Sanctum, etc). 
Deve-se implementar o modelo entidade-relacionamento a seguir: 
 Entidade produto: 
i. 
Nome (Máximo de 50 caracteres) 
ii. 
iii. 
Descrição (Máximo de 200 caracteres) 
Preço (Valor positivo, double) 
iv. Data de validade (Não pode ser anterior à data atual) 
v. Imagem (upload de imagem, nome único do arquivo) 
vi. Categoria relacionada 
 Entidade categoria: 
i. Nome: (máximo de 100 caracteres) 
O projeto também deve fazer: 
 Listar todos os produtos cadastrados. 
 Deve ser possível editar e excluir um produto. 
 Implementar paginação na listagem de produtos. 
 Implementar busca por nome e descrição. 
2. Outros detalhes técnicos: 
a. O back-end apenas fornece endpoints com retorno em JSON. 
b. Uso de Docker para executar o projeto apenas com o comando “docker
composer up -d” 
c. O projeto deve estar organizado em uma das arquiteturas de camadas 
(MVC, Arquitetura Limpa, Hexagonal) 
d. Testes Unitário 
e. Respeitar boas práticas no desenvolvimento de API 
3. Documentação: 
a. Como configurar e executar o projeto. 
b. Qualquer biblioteca ou ferramenta de terceiros utilizada e por quê. 
4. Diferenciais Adicionais: 
a. Uso de Docker para executar o projeto apenas com o comando “docker- 
composer up -d” 
b. Implementação de um sistema ACL ( Acess Control List ) no back- end. 
