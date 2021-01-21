# ChurrasTrinca

Para usar a api no swagger é preciso autenticar com token Bearer

```
{
  "name": "Thomaz",
  "email": "queroChurras@trinca.com",
  "password": "trinca123"
}
```

Copie o json acima, e cole no corpo como na imagem abaixo, depois basta clicar em **execute** : 

![image](https://user-images.githubusercontent.com/58439854/105412700-d1dd3000-5c13-11eb-8334-f65a6c732fc5.png)

Depois de logar ia aparecer uma mensagem, copie o acess token sem as aspas :

![image](https://user-images.githubusercontent.com/58439854/105412853-09e47300-5c14-11eb-8093-f57eff2b4ea9.png)

Depois no topo da tela, clique em **Authorize**, abaixo do campo value, escreve "Bearer" e cole o acess token : obs: escreva assim = **Bearer {token}**, não esqueca do espaço depois do *bearer*

Depois clique em authorize e pode usar o create da api

![image](https://user-images.githubusercontent.com/58439854/105413174-7e1f1680-5c14-11eb-95b0-f9999433a291.png)