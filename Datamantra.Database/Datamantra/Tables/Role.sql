CREATE TABLE [Datamantra].[Role] (
    [RoleId]       INT           NOT NULL,
    [RoleName]     NVARCHAR (50) NULL,
    [CreatedBy]    NCHAR (8)     NULL,
    [CreatedDate]  DATETIME      NULL,
    [ModifiedBy]   NCHAR (8)     NULL,
    [ModifiedDate] DATETIME      NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

