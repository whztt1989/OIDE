

Important to get the SQLLite provider found!
in main exe, App.config


 <entityFramework>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.SqlConnectionFactory, EntityFramework" />
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
      <provider invariantName="System.Data.SQLite.EF6" type="System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6, Version=1.0.94.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139" />
      <provider invariantName="System.Data.SQLite" type="System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6, Version=1.0.94.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139" />
    </providers>
  </entityFramework>
  <system.data>
    <DbProviderFactories>
      <remove invariant="System.Data.SQLite" />
      <add name="SQLite Data Provider" invariant="System.Data.SQLite" description=".Net Framework Data Provider for SQLite" type="System.Data.SQLite.SQLiteFactory, System.Data.SQLite, Version=1.0.94.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139" />
      <remove invariant="System.Data.SQLite.EF6" />
      <add name="SQLite Data Provider (Entity Framework 6)" invariant="System.Data.SQLite.EF6" description=".Net Framework Data Provider for SQLite (Entity Framework 6)" type="System.Data.SQLite.EF6.SQLiteProviderFactory, System.Data.SQLite.EF6, Version=1.0.94.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139" />
    </DbProviderFactories>
  </system.data>
