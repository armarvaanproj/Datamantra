CREATE TABLE [Datamantra].[User] (
    [UserId]       BIGINT         NOT NULL,
    [UserName]     NVARCHAR (50)  NULL,
    [Password]     NVARCHAR (50)  NULL,
    [Designation]  NVARCHAR (50)  NULL,
    [Image]        NVARCHAR (150) NULL,
    [Testimony]    NVARCHAR (250) NULL,
    [RoleId]       INT            NULL,
    [IsActive]     Bit      NOT NULL,
    [Rating]       NCHAR (10)     NULL,
    [CreatedBy]    NCHAR (8)      NULL,
    [CreatedDate]  DATETIME       NULL,
    [ModifiedBy]   NCHAR (8)      NULL,
    [ModifiedDate] DATETIME       NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

