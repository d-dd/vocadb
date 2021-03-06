﻿

function initPage(repoFactory, listId) {

	$("#tabs").tabs();
	$("#deleteLink").button({ icons: { primary: 'ui-icon-trash'} });

	var songListRepo = repoFactory.songListRepository();
	var songRepo = repoFactory.songRepository();

	var viewModel = new vdb.viewModels.songList.SongListEditViewModel(songListRepo, songRepo, listId);
	viewModel.init(function () {
		ko.applyBindings(viewModel);
	});

}