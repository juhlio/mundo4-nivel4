# Relatório de Atividade no Microsoft Azure

## 1. Introdução

Este relatório documenta o desenvolvimento de uma aplicação web para a gestão de um sistema de transporte. A aplicação foi desenvolvida utilizando ASP.NET Core Razor Pages, com foco na criação, edição e exclusão de registros de clientes, motoristas e ordens de serviço. O objetivo principal do projeto é facilitar a administração e operação do sistema de transporte, permitindo uma interface amigável para os usuários gerenciarem suas operações diárias. Descreve as etapas realizadas para a criação e configuração de uma Máquina Virtual (VM), configuração de regras de segurança de rede, criação e manipulação de um banco de dados SQL no Azure, e implementação de operações CRUD (Create, Read, Update, Delete) para a LogiMove Transportes. O objetivo é digitalizar as operações da empresa de logística, substituindo processos baseados em papel por soluções digitais.

## 2. Criação de Máquina Virtual no Azure

### Procedimentos
1. **Nome da Máquina Virtual**: VM-LogiMove.
2. **Região**: Brasil Sul.
3. **Sistema Operacional**: Windows Server 2019.
4. **Tamanho da Máquina Virtual**: Standard_D2s_v3 (2 vCPUs, 8 GB RAM).
5. **Opções de Autenticação**: Nome de usuário e senha.
6. **Revisão e Criação**:
   - Verificadas todas as configurações.
   - Clique em "Revisar + criar".
7. **Verificação Final**:
   - Conferência das configurações na página de revisão.
   - Clique em "Criar".
8. **Conclusão da Criação**:
   - Notificação recebida após alguns minutos indicando a conclusão da criação da VM.
9. **Verificação da VM**:
   - Acesso ao painel de controle do Azure e confirmação da VM listada nos "Recursos".
   
### Importância do Nome da Máquina Virtual

O nome atribuído à máquina virtual, neste caso "VM-LogiMove", serve não apenas como um identificador único dentro da infraestrutura do Azure, mas também como uma convenção de nomenclatura que ajuda na identificação rápida e organização dos recursos. É recomendável escolher um nome descritivo e relevante para facilitar a administração e a manutenção futura da infraestrutura de TI.

## 3. Configuração de Regras de Segurança de Rede

### Procedimentos
1. **Acesso ao Portal do Azure**: Navegador Web.
2. **Seleção da VM**: VM criada anteriormente.
3. **Configurações de Rede**: Menu lateral esquerdo.
4. **Grupo de Segurança de Rede**:
   - Clique em "Criar regra de portas" para criar uma nova “Regra de portas de entrada”.
5. **Configuração da Regra**:
   - **Origem**: Any.
   - **Intervalos de Porta de Origem**: * (todas as portas).
   - **Destino**: Any.
   - **Serviço**: HTTP.
   - **Intervalos de Porta de Destino**: 80.
   - **Protocolo**: TCP.
   - **Ação**: Permitir.
   - **Prioridade**: 300.
   - **Nome da Regra**: Allow-HTTP.
   - **Descrição**: Permitir tráfego HTTP.
6. **Criação da Regra**:
   - Clique em "Adicionar".

## 4. Criação de Banco de Dados SQL no Azure

### Procedimentos
1. **Acesso ao Portal do Azure**: Navegador Web.
2. **Criação do Recurso**: SQL Database.
3. **Configuração do Banco de Dados**:
   - **Nome do Banco de Dados**: LogiMoveDB.
   - **Grupo de Recursos**: LogiMoveResourceGroup.
   - **Servidor do Banco de Dados**: Novo servidor criado.
   - **Configurações de Segurança**: Autenticação SQL, usuário administrador e senha definidos.
4. **Revisão e Criação**:
   - Clique em "Revisar + criar".
   - Verificação final das configurações e clique em "Criar".
5. **Conclusão da Criação**:
   - Notificação recebida após alguns minutos indicando a conclusão da criação do banco de dados.

## 5. Conexão ao Banco de Dados

### Procedimentos
1. **Acesso ao Azure Cloud Shell**:
   - Seleção do ambiente Bash.
2. **Configuração dos Valores Padrão**:
   - `az configure --defaults group=LogiMoveResourceGroup sql-server=LogiMoveServer`.
3. **Listagem dos Bancos de Dados**:
   - `az sql db list`.
4. **Detalhes do Banco de Dados**:
   - `az sql db show --name LogiMoveDB`.
5. **Obtenção da String de Conexão**:
   - `az sql db show-connection-string --client sqlcmd --name LogiMoveDB`.

## 6. Operações CRUD no Banco de Dados

### Procedimentos
1. **Conexão ao Banco de Dados**:
   - Uso da ferramenta `sqlcmd` com a string de conexão obtida.
2. **Criação da Tabela Drivers**:
   ```sql
   CREATE TABLE Drivers (
       DriverID INT PRIMARY KEY,
       Nome VARCHAR(100),
       CNH VARCHAR(20),
       Endereço VARCHAR(200),
       Contato VARCHAR(50)
   );
   GO
   ```
3. **Verificação da Existência da Tabela**:
   ```sql
   SELECT name FROM sys.tables;
   GO
   ```
4. **Inserção de Dados**:
   ```sql
   INSERT INTO Drivers (DriverID, LastName, FirstName, OriginCity) VALUES (754, 'Silva', 'João', 'Rio de Janeiro');
   GO
   ```
5. **Leitura de Dados**:
   ```sql
   SELECT DriverID, OriginCity FROM Drivers;
   GO
   ```
6. **Atualização de Dados**:
   ```sql
   UPDATE Drivers SET OriginCity='São Paulo' WHERE DriverID=754;
   GO
   ```
7. **Exclusão de Dados**:
   ```sql
   DELETE FROM Drivers WHERE DriverID=754;
   GO
   ```
8. **Verificação de Tabela Vazia**:
   ```sql
   SELECT COUNT(*) FROM Drivers;
   GO
   ```

## 7. Resultados Esperados

- **Melhoria Operacional**: Digitalização das operações da LogiMove Transportes, proporcionando maior eficiência e controle sobre os processos.

- **Escalabilidade**: Soluções implementadas preparadas para suportar o crescimento futuro da empresa.

- **Segurança**: Medidas de segurança implementadas garantem a proteção dos dados e informações do sistema.

### Criação de Máquina Virtual
- VM criada e configurada corretamente, acessível através do painel de controle do Azure.

### Regras de Segurança
- Regra de segurança configurada com sucesso permitindo acesso HTTP à VM.

### Banco de Dados SQL
- Banco de dados SQL no Azure criado e configurado corretamente, com regras de firewall ajustadas para permitir acesso.

### Operações CRUD
- Tabelas criadas e manipuladas corretamente com operações CRUD executadas com sucesso, verificando a integridade e funcionamento das operações.

### Conclusão
- Com as atividades realizadas, a LogiMove Transportes está pronta para migrar para um sistema digital, melhorando a coordenação, rastreamento de remessas e a satisfação dos clientes. As soluções implementadas garantem escalabilidade e segurança, preparando a empresa para o crescimento futuro, sistema de gestão baseado em ASP.NET Core Razor Pages representa um avanço significativo para a LogiMove Transportes, modernizando suas operações e melhorando a experiência do cliente. As soluções adotadas oferecem uma plataforma robusta e escalável, alinhada com as melhores práticas de segurança.
