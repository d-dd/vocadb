﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using VocaDb.Model.Service.Helpers;

namespace VocaDb.Tests.Service.Helpers {

	[TestClass]
	public class ArtistExternalUrlParserTests {

		private void TestGetExternalUrl(string input, string expected) {
			
			var result = new ArtistExternalUrlParser().GetExternalUrl(input);

			Assert.AreEqual(expected, result, input);

		}

		[TestMethod]
		public void Partial() {
			
			TestGetExternalUrl("mylist/6667938", "http://www.nicovideo.jp/mylist/6667938");

		}

		[TestMethod]
		public void Full() {
			
			TestGetExternalUrl("http://www.nicovideo.jp/mylist/6667938", "http://www.nicovideo.jp/mylist/6667938");

		}

		[TestMethod]
		public void NoMatch_SameDomain() {
			
			TestGetExternalUrl("http://www.nicovideo.jp", null);

		}

		[TestMethod]
		public void NoMatch_DifferentDomain() {
			
			TestGetExternalUrl("http://test.vocadb.net/mylist/6667938", null);

		}

	}

}