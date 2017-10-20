require("CheckList/checker")

task_checklist = {
	tTarget=Data.task,
	tCheckList={
		{funName="CheckUnique", column="id", params=nil},
		{funName="CheckRange", column="type", params={1,6}},
		{funName="CheckRange", column={"condition", "conditionID"}, params={0,18}},
		{funName="CheckIDExistAndCanBeZero", column="frontTask", params={Data.task, "id"}},
		{funName="CheckIDExistAndCanBeZero", column="followTask", params={Data.task, "id"}},
		{funName="CheckIDExistAndCanBeZero", column="acceptNpc", params={Data.npc, "id"}},
		{funName="CheckIDExistAndCanBeZero", column="commitNpc", params={Data.npc, "id"}},
	}
}
Checker.Start(task_checklist)

npc_checklist = {
	tTarget=Data.npc,
	tCheckList={
		{funName="CheckIDExistAndCanBeZero", column="sceneID", params={Data.sceneConf, "id"}},
		{funName="CheckIDExistAndCanBeZero", column="npcModelID", params={Data.npcModel, "id"}},
		{funName="CheckRange", column="type", params={0,4}},
		--{funName="CheckIDExistAndCanBeZero", column="toTransfer", params={"npc.sceneID=sceneConf.id=sceneConf.objPosFile", "id"}},
	}
}
Checker.Start(npc_checklist)

skill_checklist = {
	tTarget=Data.skill,
	tCheckList={
		{funName="CheckRange", column="skillType", params={0,4}},
		{funName="CheckIDExistAndCanBeZero", column="comboList", params={Data.skill, "id"}},
		{funName="CheckIDExistAndCanBeZero", column={"subCall", "id"}, params={Data.subSkill, "id"}},
	}
}
Checker.Start(skill_checklist)
