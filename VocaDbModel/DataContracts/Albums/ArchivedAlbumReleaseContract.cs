﻿using System.Runtime.Serialization;
using VocaDb.Model.DataContracts.ReleaseEvents;
using VocaDb.Model.Domain;
using VocaDb.Model.Domain.Albums;

namespace VocaDb.Model.DataContracts.Albums {

	[DataContract(Namespace = Schemas.VocaDb)]
	public class ArchivedAlbumReleaseContract : IAlbumRelease {

		int IAlbumRelease.EventId => ReleaseEvent?.Id ?? 0;
		string IAlbumRelease.EventName => ReleaseEvent?.NameHint;

		public ArchivedAlbumReleaseContract() { }

		public ArchivedAlbumReleaseContract(AlbumRelease release) {

			ParamIs.NotNull(() => release);

			CatNum = release.CatNum;
			ReleaseDate = (release.ReleaseDate != null ? new OptionalDateTimeContract(release.ReleaseDate) : null);

			if (ReleaseDate != null)
				ReleaseDate.Formatted = string.Empty;

			ReleaseEvent = ObjectRefContract.Create(release.ReleaseEvent);

		}

		[DataMember]
		public string CatNum { get; set; }

		[DataMember]
		public OptionalDateTimeContract ReleaseDate { get; set; }

		IOptionalDateTime IAlbumRelease.ReleaseDate => ReleaseDate;

		[DataMember]
		public ObjectRefContract ReleaseEvent { get; set; }

	}

}
