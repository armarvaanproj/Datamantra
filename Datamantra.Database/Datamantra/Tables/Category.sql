CREATE TABLE [Datamantra].[Category] (
    [CategoryId]    BIGINT       IDENTITY (1, 1) NOT NULL,
    [CategoryName]  VARCHAR (50) NULL,
    [SubCategoryId] BIGINT       NULL,
    [CreatedBy]     NCHAR (8)    NULL,
    [CreatedDate]   DATETIME     NULL,
    [ModifiedBy]    NCHAR (8)    NULL,
    [ModifiedDate]  DATETIME     NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);

