# Requisitos do Project TD

Este documento contém os requisitos do **Project TD**, um jogo de Tower Defense com gerenciamento de recursos e elementos de ideologias extremistas e polarizadas.



## Resumo de Requisitos

### Requisitos Essenciais
- [ ] Mecânicas de Tower Defense com mapa em grade hexagonal.
- [ ] Gestão de recursos essenciais: alimentos, materiais de construção, remédios.
- [ ] Modos de jogo: Sobrevivência e Campanha.
- [ ] Implementação de sistema de doença e vacinas.
- [ ] Variedade de inimigos e defesas com efeitos de status.
- [ ] Fases predefinidas com aumento de dificuldade.
- [ ] Interface de usuário básica (menus e controle do fluxo do jogo).

---

## Requisitos Essenciais

### 1. Mecânicas Básicas
- [ ] **Tower Defense**: Implementar um sistema básico de defesa de um ponto central contra ondas de inimigos.
  - [ ] Inimigos surgem de pontos de spawn e seguem caminhos até o centro.
  - [ ] O jogador pode construir e melhorar defesas ao longo desses caminhos.
  
- [ ] **Grade Hexagonal**: O mapa será gerado com uma grade de hexágonos onde as defesas e produções serão colocadas.
  - [ ] Sistema de terreno variado, com obstáculos naturais e hexágonos especializados.

- [ ] **Gestão de Recursos**: O jogador deve gerenciar os seguintes tipos de recursos:
  - [ ] **Alimentos**: Sustentar a população e a produção.
  - [ ] **Materiais de construção**: Necessários para construir e melhorar defesas.
  - [ ] **Remédios**: Gerir surtos de doenças e proteger a produção e defesas.

- [ ] **Efeitos de Status**: Inimigos e defesas podem sofrer e aplicar efeitos de status como:
  - [ ] Envenenamento
  - [ ] Congelamento
  - [ ] Paralisação

### 2. Modos de Jogo
- [ ] **Sobrevivência**: Resistir a ondas contínuas de inimigos.
  - [ ] Definir um número fixo de ondas para sobreviver.
  
- [ ] **Campanha**: Avançar através de fases predefinidas, com diferentes desafios e objetivos.
  - [ ] Proteger áreas críticas, conquistar territórios e enfrentar facções inimigas.

### 3. Sistema de Doença e Vacinas
- [ ] **Doença**: Adicionar uma doença que afeta a produção e defesas.
  - [ ] A doença deve poder surgir aleatoriamente ou ser introduzida pelos inimigos.
  
- [ ] **Vacinas**: O jogador pode pesquisar e implementar vacinas para reduzir o impacto da doença.
  - [ ] Resistência de facções ou populações à vacinação deve influenciar a produção e a eficácia das defesas.

### 4. Defesas
- [ ] **Tipos de Torres**: Implementar torres com diferentes funções.
  - [ ] Torres de ataque rápido
  - [ ] Torres de efeito de status
  - [ ] Barricadas/Muros

### 5. Inimigos
- [ ] **Variedade de Inimigos**: Criar inimigos com diferentes características.
  - [ ] Inimigos rápidos
  - [ ] Inimigos resistentes
  - [ ] Inimigos com habilidades especiais (enfraquecer defesas, causar surtos de doença)

### 6. Sistema de Fases
- [ ] **Fases Predefinidas**: Cada fase deve apresentar um mapa com um ponto de spawn e um objetivo central.
  - [ ] As fases terão níveis de dificuldade crescente.
  - [ ] Cada fase pode incluir missões como sobreviver a X ondas ou conquistar uma área.

### 7. Interface de Usuário (UI)
- [ ] **Menus Principais**: Implementar menus básicos.
  - [ ] Menu Principal
  - [ ] Menu de Pausa
  - [ ] Tela de Game Over

---

## Requisitos Extras

### 1. População e Gerenciamento Social
- [ ] **População Gerenciável**: Adicionar uma população que influencia a produção e as defesas.
  - [ ] O jogador deve alocar recursos para manter a população saudável e produtiva.

- [ ] **Impacto da Ideologia**: Dependendo das decisões de gestão (capitalismo vs socialismo), a população pode reagir de maneira diferente aos surtos de doença e à produção de recursos.

### 2. Clima e Terreno Dinâmicos
- [ ] **Terreno Dinâmico**: Adicionar mecânicas onde o clima ou mudanças no terreno afetam a produção e a defesa.
  - [ ] Exemplo: Nevascas afetam a eficiência de defesas de fogo e aumentam a eficácia de defesas de gelo.

### 3. Extensão de Facções e Inimigos
- [ ] **Facções Inimigas**: Introduzir diferentes facções inimigas com estilos de ataque e resistência baseados em ideologias opostas (ex: mecanistas vs darwinistas).
  - [ ] Cada facção deve ter suas próprias habilidades especiais e comportamento único.

### 4. Progressão entre Fases (Metagame)
- [ ] **Melhorias Permanentes**: Implementar um sistema de progressão onde o jogador pode desbloquear melhorias permanentes após completar fases.
  - [ ] Exemplo: Melhorias para as torres ou aumento na produção de recursos.

### 5. Temas e Estilos de Jogo
- [ ] **Escolha de Temas**: O jogador ou o sistema pode escolher entre diferentes estilos e temáticas para o jogo:
  - [ ] **Dark Fantasy**
  - [ ] **Distopia Tecnológica**
  - [ ] **Ficção Científica Espacial**
  - [ ] **Steampunk vs. Biotecnologia**

### 6. Multiplayer
- [ ] **Modo PvP ou Cooperativo**: Implementar um modo multiplayer onde os jogadores podem competir por recursos ou se unir para enfrentar inimigos em comum.



### Requisitos Extras
- [ ] População gerenciável e impacto ideológico.
- [ ] Terreno e clima dinâmicos.
- [ ] Facções inimigas com comportamentos únicos.
- [ ] Progressão entre fases com melhorias permanentes.
- [ ] Escolha de temas de ambientação.
- [ ] Implementação de modo multiplayer (PvP ou cooperativo).
