using System.Collections.Generic;
using PaliCanon.Contracts.Verse;

namespace PaliCanon.Contracts.Chapter
{
    public interface IChapterEntity
    {
        string BookCode { get; }

        string Title { get; set; }

        int ChapterNumber { get; set; }

    }
}