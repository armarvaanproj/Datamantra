CREATE TABLE [Datamantra].[CourseAuthorMapping] (
    [AuthorId]     BIGINT    IDENTITY (1, 1) NOT NULL,
    [CourseId]     BIGINT    NULL,
    [UserId]       BIGINT    NULL,
    [CreatedBy]    NCHAR (8) NULL,
    [CreatedDate]  DATETIME  NULL,
    [ModifiedBy]   NCHAR (8) NULL,
    [ModifiedDate] DATETIME  NULL,
    CONSTRAINT [PK_CourseAuthorMapping] PRIMARY KEY CLUSTERED ([AuthorId] ASC)
);

