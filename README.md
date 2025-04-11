# SmartControlPlayer

**SmartControlPlayer** é um plugin modular inteligente para o jogo Repo, integrando escalonamento dinâmico, tradução, atualização e DevMode.

## Principais Funcionalidades

- **SmartConfigManager**: Gerencia todos os Config.Bind e interações.
- **MapImmunitySystem**: Detecta e impõe imunidade de mapa, ignorando configurações do plugin quando necessário.
- **TranslationHandler**: Carrega traduções a partir de arquivos JSON na pasta Lang.
- **DevMode UI**: Interface ativada via F10 para testes e execução de comandos dinâmicos.
- **UpdateChecker**: Verifica atualizações via Gist do GitHub.
- **Hooks e Dry Run**: Suporte a patches reversíveis e simulação de alterações.
- **MiniIA (opcional)**: Sugestões automatizadas baseadas em heurísticas (desativada por padrão).
- **Logging detalhado**: Registro de todas as interações no log localizado em `%APPDATA%/Local/Repo/SmartControlPlayer/smartlog.log`.

## Instruções de Compilação

- Utilize o .NET Standard 2.0.
- Configure o projeto para uso com BepInEx.
- Coloque os arquivos de tradução em `%APPDATA%/Local/Repo/SmartControlPlayer/Lang/`.

## Configurações Iniciais

- Edite o arquivo de configuração gerado pelo BepInEx para ajustar os binds.
- O UpdateChecker usará um placeholder para o link; atualize conforme necessário.

