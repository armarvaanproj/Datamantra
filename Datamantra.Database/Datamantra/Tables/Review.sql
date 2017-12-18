CREATE TABLE [Datamantra].[Review] (
    [ReviewId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId]       BIGINT         NULL,
    [CourseId]     BIGINT         NULL,
    [Comments]     NVARCHAR (250) NULL,
    [CreatedBy]    NCHAR (8)      NULL,
    [CreatedDate]  DATETIME       NULL,
    [ModifiedBy]   NCHAR (8)      NULL,
    [ModifiedDate] DATETIME       NULL,
    CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED ([ReviewId] ASC)
);

