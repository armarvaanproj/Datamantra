CREATE TABLE [Datamantra].[Course] (
    [CourseId]         BIGINT        NOT NULL,
    [CourseName]       VARCHAR (50)  NULL,
    [Rating]           INT           NULL,
    [Price]            DECIMAL (18)  NULL,
    [DiscountedPrice]  DECIMAL (18)  NULL,
    [EnrolledCount]    BIGINT        NULL,
    [Image]            NCHAR (10)    NULL,
    [ShortDescription] VARCHAR (250) NULL,
    [LongDescription]  VARCHAR (250) NULL,
    [CreatedBy]        NCHAR (8)     NULL,
    [CreatedDate]      DATETIME      NULL,
    [ModifiedBy]       NCHAR (8)     NULL,
    [ModifiedDate]     DATETIME      NULL,
    [CategoryId]       NCHAR (10)    NULL,
    CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED ([CourseId] ASC)
);

