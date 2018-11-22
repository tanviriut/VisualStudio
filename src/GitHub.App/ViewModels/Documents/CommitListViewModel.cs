﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitHub.ViewModels.Documents
{
    /// <summary>
    /// Displays a list of commit summaries in a pull request timeline.
    /// </summary>
    public class CommitListViewModel : ViewModelBase, ICommitListViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommitListViewModel"/> class.
        /// </summary>
        /// <param name="commits">The commits to display.</param>
        public CommitListViewModel(params ICommitSummaryViewModel[] commits)
        {
            if (commits.Length == 0)
            {
                throw new NotSupportedException("Cannot create a CommitListViewModel with 0 commits.");
            }

            Commits = commits;
            Author = Commits[0].Author;
            AuthorCaption = BuildAuthorCaption();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommitListViewModel"/> class.
        /// </summary>
        /// <param name="commits">The commits to display.</param>
        public CommitListViewModel(IEnumerable<ICommitSummaryViewModel> commits)
        {
            Commits = commits.ToList();

            if (Commits.Count == 0)
            {
                throw new NotSupportedException("Cannot create a CommitListViewModel with 0 commits.");
            }

            Author = Commits[0].Author;
            AuthorCaption = BuildAuthorCaption();
        }

        /// <inheritdoc/>
        public IActorViewModel Author { get; }

        /// <inheritdoc/>
        public string AuthorCaption { get; }

        /// <inheritdoc/>
        public IReadOnlyList<ICommitSummaryViewModel> Commits { get; }

        string BuildAuthorCaption()
        {
            var result = new StringBuilder();

            if (Commits.Any(x => x.Author.Login != Author.Login))
            {
                result.Append(Resources.AndOthers);
                result.Append(' ');
            }

            result.Append(Resources.AddedSomeCommits);
            return result.ToString();
        }
    }
}
