# Game Design Document (Tower Defense)

## 1. Overview
O Project TD é um jogo de Tower Defense com elementos de gerenciamento de recursos e sobrevivência, ambientado em um mundo fictício em que ideologias polarizadas influenciam a forma como os recursos são gerenciados e as defesas são construídas. O jogador precisa proteger uma área central contra ondas de inimigos, gerenciar crises, como surtos de doenças, e equilibrar suas escolhas entre diferentes ideologias, que impactam tanto o sistema de produção quanto as defesas.

### 1.1. Conflito de Ideologias e Temas

O jogo incorpora a luta entre sistemas de produção e ideologias opostas, como:

* **Capitalismo:** Focado na acumulação de recursos, maximização de produção e exploração. O sistema permite grande crescimento, mas gera desigualdades e pode esgotar os recursos mais rapidamente.
* **Socialismo:** Focado na distribuição igualitária dos recursos e produção coletiva. Esse sistema pode manter estabilidade e justiça social, mas tem crescimento mais lento e exige mais planejamento e coordenação.

Além disso, o jogo apresenta o tema de vacinas e doenças:

* **Doença:** Um surto de doença pode impactar a produção e a eficácia das defesas, forçando o jogador a tomar decisões sobre como gerenciar o sistema de saúde e vacinar a população.
* **Vacinas:** O jogador pode implementar sistemas de vacinas que protegem a população, mas isso pode gerar resistência de algumas facções, que preferem confiar em suas próprias soluções ou mesmo negar a existência da doença.

### 1.2. Modos de Jogo
* **Sobrevivência:** O jogador deve resistir a ataques contínuos, construindo defesas e gerenciando produções para proteger o centro de operações. A doença e os conflitos de ideologias extremistas criam desafios adicionais.
* **Campanha:** O jogador deve avançar em um território, conquistando novas áreas e enfrentando facções que representam ideologias distintas, com foco na luta por recursos, territórios e controle ideológico.
----------------
## 2. Mecânicas Principais (Gameplay)

### 2.1. Mapa e Terreno
O mapa será uma grade (Blocos ou hexagonos), onde o jogador poderá posicionar defesas, estruturas de produção e gerenciar recursos. A movimentação dos inimigos será livre, mas o jogador precisará usar o terreno a seu favor para criar estratégias de defesa e produção eficazes.

#### 2.1.1. Tipos de Terreno

* **Terrenos Normais:** Permitem a construção de defesas e produções.
* **Obstáculos Naturais:** Como montanhas ou rios, que bloqueiam o caminho dos inimigos e afetam a estratégia.
* **Terrenos Especializados:** Fornecem bônus defensivos ou produtivos. Por exemplo, áreas florestais podem fornecer melhores recursos para a produção de remédios.

### 2.2. Defesas
O jogador constrói defesas para impedir o avanço dos inimigos. Essas defesas podem ser melhoradas conforme o jogo avança e podem sofrer status (como congelamento, envenenamento, etc.).

#### 2.2.1. Tipos de Defesas
* **Torres de ataque rápido:** Causam dano rápido e contínuo.
* **Torres de efeito:** Aplicam efeitos de status nos inimigos, como congelar ou envenenar.
* **Barricadas/Muros:** Impedem o avanço dos inimigos, mas podem ser destruídas.

### 2.3. Inimigos
Os inimigos surgem de pontos de spawn e seguem em direção ao centro de operações. Cada inimigo pode ser influenciado pelas ideologias do mundo e pelos status que o jogador aplica.

#### 2.3.1. Tipos de Inimigos
* **Inimigos rápidos:** Avançam rapidamente, mas são vulneráveis a defesas de longo alcance.
* **Inimigos resistentes:** Avançam devagar, mas têm alta resistência a ataques diretos.
* **Inimigos com habilidades especiais:** Podem enfraquecer defesas ou contaminar a produção com doenças.

### 2.4. Produção e Gerenciamento de Recursos
Além de construir defesas, o jogador precisa gerenciar a produção de recursos necessários para continuar expandindo as defesas e mitigando os efeitos da doença. As produções podem ser impactadas pelas ideologias escolhidas, criando dilemas estratégicos.

#### 2.4.1. Tipos de Recursos
* **Alimentos:** Necessários para sustentar a produção e as operações básicas.
* **Materiais de construção:** Utilizados para construir e melhorar defesas e infraestruturas.
* **Remédios:** Necessários para mitigar surtos de doenças e manter a população protegida.

#### 2.4.2. Impacto das Doenças
As doenças podem reduzir a eficácia da produção de recursos e enfraquecer as defesas. O jogador precisa tomar decisões sobre a aplicação de vacinas e alocar recursos para combater surtos.

### 2.5. Fases e Progressão
Cada fase do jogo será um mapa predefinido, com pontos de spawn para os inimigos e um centro de operações que o jogador deve proteger. A dificuldade aumenta conforme o jogador avança, adicionando mais inimigos, diferentes tipos de terrenos e novos efeitos de status.

#### 2.5.1. Missões
Cada fase pode ter missões variadas, como:

* Proteger o centro de operações por um número de ondas.
* Gerenciar surtos de doenças enquanto mantém a produção.
* Conquistar e defender áreas controladas por facções inimigas.
--------------------
## 3. História e Lore

O Project TD ocorre em um mundo onde ideologias extremistas competem pelo controle dos recursos e territórios. As facções adotam diferentes abordagens para o gerenciamento da sociedade e da produção. Doenças misteriosas surgem periodicamente, forçando as facções a tomar decisões difíceis sobre como proteger suas populações e manter a produção estável.

* **Capitalistas:** Acreditam no crescimento econômico sem limites, focando em maximizar a produção a qualquer custo. Porém, isso leva à exploração de recursos naturais e sociais.
* **Socialistas:** Focam na distribuição equitativa de recursos, evitando a exploração, mas lutam para manter um crescimento constante e responder rapidamente às crises.

Além disso, um surto de uma doença misteriosa coloca essas ideologias em cheque, especialmente com a resistência a soluções como vacinas. O jogador precisa equilibrar as pressões políticas e a sobrevivência física da sua população.

-------------------
## 4. Referências
* **Leviatã (Thomas Hobbes):** Conflito entre facções com ideologias opostas, representando tecnologia mecânica e biológica.
* **A Companhia Negra (Glen Cook):** Cenário de dark fantasy com guerra e moralidade ambígua.
Timberborn: Gerenciamento de produção e recursos com blocos predefinidos e IA com prioridades de ações.
* **Plants vs. Zombies:** Defesa estratégica e mecânica de ondas de inimigos.
* **Civilization:** Gerenciamento de recursos e escolhas ideológicas.
* **Final Fantasy Tactics:** Combate baseado em grid com diferentes unidades e habilidades.
* **Bloons TD 5:** Tower Defense tradicional com evolução e especialização de defesas.
* **Crowntakers:** Mais simples, mas usa HEX e tem troca de cenários e abertura do mesmo. Da para andar e lutar no mesmo cenário.
-------------------
## 5. Opções de Temas

O tema ainda será definido, mas aqui estão algumas opções para orientar o desenvolvimento:

* Dark Fantasy: Um mundo sombrio onde facções mágicas e biológicas lutam pelo controle, inspirando-se na Companhia Negra.
* Distopia Tecnológica: Facções de capitalismo corporativo versus socialismo tecnológico lutam pelo controle de recursos em um futuro pós-apocalíptico.
* Mix de Magia e Mecânica: Um universo onde facções misturam magia e tecnologia para construir máquinas de guerra e criaturas biológicas, como no Leviatã de Thomas Hobbes.
* Ficção Científica no Espaço: Facções de colônias espaciais com sistemas políticos divergentes (capitalismo galáctico versus comunas espaciais) lutam pelo controle de planetas ricos em recursos.
* Steampunk vs. Biotecnologia: Facções com máquinas a vapor avançadas enfrentam facções que utilizam criaturas biológicas modificadas geneticamente.
-------------------

## 6. Design Patterns e Arquitetura

### 6.1. Máquina de Estados (State Pattern)
* **Estado do jogo:** Controla os principais estados como Menu, Jogo, Pause, Game Over.
* **Modos de jogo:** Define o comportamento específico dos modos (Sobrevivência, Campanha).
* **IA dos Inimigos:** Utiliza estados como "Patrulhar", "Atacar", "Congelar", com transições dinâmicas baseadas nas condições do jogo.
### 6.2. Singleton Pattern
* **GameManager:** Controla o fluxo principal do jogo, garantindo que uma única instância esteja gerenciando os estados do jogo.

### 6.3. Factory Pattern

* Usado para a criação de inimigos e defesas, permitindo uma fácil expansão das variações de torres e tipos de inimigos.

-------------------
## 7. Mecânicas e Funcionalidades

### 7.1. Recursos e Defesas
* O jogador coleta e administra recursos (alimentos, materiais, remédios) que permitem a construção e melhoria de defesas e mitigam os efeitos das doenças.
* As defesas podem ser melhoradas conforme o jogador progride, ganhando novos atributos e efeitos especiais.
### 7.2. Status e Efeitos
* Inimigos e defesas podem ter efeitos de status que influenciam diretamente o desempenho (envenenamento, congelamento, etc.).
* Os efeitos de status adicionam uma camada de estratégia, forçando o jogador a adaptar suas defesas.
<!-- ## 3. IA
Cada facção possui IA com comportamento baseado na sua ideologia:
- **Facção Capitalista**: Acumulação de recursos e construção rápida de defesas.
- **Facção Comunista**: Distribuição equitativa de recursos e foco em unidades de produção.

## 4. Estados do Jogo
- **Estado de Planejamento**: O jogador organiza suas defesas.
- **Estado de Ataque**: A IA inimiga tenta destruir as defesas do jogador.

## 5. Tema e Analogias
- **Capitalismo vs Comunismo**: Representado através de facções que refletem a luta por controle e distribuição de recursos.
- **Vacina e Resistência**: Uma doença misteriosa que ameaça o mundo, vacinas são criadas, mas há resistência a elas. -->
