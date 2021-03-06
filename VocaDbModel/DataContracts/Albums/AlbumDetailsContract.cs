﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using VocaDb.Model.DataContracts.PVs;
using VocaDb.Model.DataContracts.Songs;
using VocaDb.Model.DataContracts.Users;
using VocaDb.Model.Domain.Albums;
using VocaDb.Model.Domain.Globalization;
using VocaDb.Model.DataContracts.Tags;
using VocaDb.Model.Domain.Security;

namespace VocaDb.Model.DataContracts.Albums {

	[DataContract(Namespace = Schemas.VocaDb)]
	public class AlbumDetailsContract : AlbumContract {

		public AlbumDetailsContract() { }

		public AlbumDetailsContract(Album album, ContentLanguagePreference languagePreference, IUserPermissionContext userContext)
			: base(album, languagePreference) {

			ArtistLinks = album.Artists.Select(a => new ArtistForAlbumContract(a, languagePreference)).OrderBy(a => a.Name).ToArray();
			CanRemoveTagUsages = EntryPermissionManager.CanRemoveTagUsages(userContext, album);
			Description = album.Description;
			Discs = album.Songs.Any(s => s.DiscNumber > 1) ? album.Discs.Select(d => new AlbumDiscPropertiesContract(d)).ToDictionary(a => a.DiscNumber) : new Dictionary<int, AlbumDiscPropertiesContract>(0);
			OriginalRelease = (album.OriginalRelease != null ? new AlbumReleaseContract(album.OriginalRelease) : null);
			Pictures = album.Pictures.Select(p => new EntryPictureFileContract(p)).ToArray();
			PVs = album.PVs.Select(p => new PVContract(p)).ToArray();
			Songs = album.Songs
				.OrderBy(s => s.DiscNumber).ThenBy(s => s.TrackNumber)
				.Select(s => new SongInAlbumContract(s, languagePreference, false)).ToArray();
			Tags = album.Tags.ActiveUsages.Select(u => new TagUsageForApiContract(u, languagePreference)).OrderByDescending(t => t.Count).ToArray();
			WebLinks = album.WebLinks.Select(w => new WebLinkContract(w)).OrderBy(w => w.DescriptionOrUrl).ToArray();

			TotalLength = Songs.All(s => s.Song != null && s.Song.LengthSeconds > 0) ? TimeSpan.FromSeconds(Songs.Sum(s => s.Song.LengthSeconds)) : TimeSpan.Zero;

		}

		[DataMember]
		public AlbumForUserContract AlbumForUser { get; set; }

		[DataMember]
		public ArtistForAlbumContract[] ArtistLinks { get; set; }

		public bool CanRemoveTagUsages { get; set; }

		[DataMember]
		public int CommentCount { get; set; }

		[DataMember]
		public EnglishTranslatedString Description { get; set; }

		[DataMember]
		public Dictionary<int, AlbumDiscPropertiesContract> Discs { get; set; }

		[DataMember]
		public int Hits { get; set; }

		[DataMember]
		public CommentForApiContract[] LatestComments { get; set; }

		[DataMember]
		public AlbumContract MergedTo { get; set; }

		[DataMember]
		public AlbumReleaseContract OriginalRelease { get; set; }

		[DataMember]
		public int OwnedCount { get; set; }

		[DataMember]
		public EntryPictureFileContract[] Pictures { get; set; }

		[DataMember]
		public PVContract[] PVs { get; set; }

		[DataMember]
		public SongInAlbumContract[] Songs { get; set; }

		[DataMember]
		public TagUsageForApiContract[] Tags { get; set; }

		[DataMember]
		public TimeSpan TotalLength { get; set; }

		[DataMember]
		public WebLinkContract[] WebLinks { get; set; }

		[DataMember]
		public int WishlistCount { get; set; }

	}

}
