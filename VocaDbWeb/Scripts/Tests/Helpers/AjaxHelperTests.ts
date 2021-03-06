﻿
module vdb.tests.helpers {

	QUnit.module("AjaxHelper");

	var testCreateUrl = (params: { [key: string]: any[]; }, expected: string) => {
		var actual = vdb.helpers.AjaxHelper.createUrl(params);
		QUnit.equal(actual, expected);
	}

	QUnit.test("single param", () => {
		testCreateUrl({ vocaloid: ["miku", "luka"] }, "vocaloid=miku&vocaloid=luka");
	});

	QUnit.test("multiple params", () => {
		testCreateUrl({ vocaloid: ["miku"], song: ["Nebula"] }, "vocaloid=miku&song=Nebula");
	});

}