Checker={}

function Checker.Reset()
	Checker.ExistList={}
end

function Checker.Start(tChecker)
	local checkerName = nil
	for key,value in pairs(_G)
	do
		if value == tChecker then
			checkerName = key
			break
		end
	end

	if tChecker.tTarget == nil then
		print(checkerName .. "���������д���󣬲����ڴ˱�")
		return
	end
	
	if tChecker.tCheckList == nil or #tChecker.tCheckList == 0 then
		print(checkerName .. "�����Ϊ��")
		return
	end
	
	Checker.Reset()
	
	print(checkerName .. "�����ɣ�")
	
	for nID, tValues in pairs(tChecker.tTarget)
	do
		for nIndex, tCheckItem in ipairs(tChecker.tCheckList)
		do
			if tCheckItem.funName == nil then
				print(checkerName .. "��" .. nIndex .. "������")
				return
			end
			local fun = Checker[tCheckItem.funName]
			
			local tValue = nil
			local szColumnName = nil
			local tCurValues = tValues
			if type(tCheckItem.column) == "table" then --˵����Ҫ��struct����list����ȥ����
				for _,columnName in ipairs(tCheckItem.column)
				do
					szColumnName = columnName
					tValue = tCurValues[columnName]
					if tValue == nil then -- ��ֱ�Ӳ��Ҳ���keyֵ���п������Ǹ�map
						for _,tValueMap in ipairs(tCurValues)
						do
							if tValueMap[columnName] ~= nil then
								if tValue == nil then
									tValue = {}
								end
								table.insert(tValue, tValueMap[columnName])
							end
						end
						
						if tValue == nil then
							print("ID:" .. nID .. ":" .. checkerName .. "��" .. nIndex .. "�в�����" .. szColumnName)
						end
					elseif type(tValue) == "table" then
						tCurValues = tValue
					end
				end
			else
				szColumnName = tCheckItem.column
				tValue = tValues[tCheckItem.column]
			end
			
			if tValue ~= nil then
				--ֵ����Ϊlist<int>,list<string>,list<float>����
				if type(tValue) == "table" then
					for _,szValue in ipairs(tValue)
					do
						local ret = fun(szColumnName, szValue, tCheckItem.params)
						if not ret then
							return;
						end
					end
				else
					local ret = fun(szColumnName, tValue, tCheckItem.params)
					if not ret then
						return;
					end
				end
			end
		end
	end
end

function Checker.CheckUnique(columnName, value)
	if Checker.ExistList[columnName] == nil then
		Checker.ExistList[columnName] = {}
	end
	local existList = Checker.ExistList[columnName]
	for _,existValue in ipairs(existList)
	do
		if existValue == value then
			print(columnName .. "��" .. value .. "ֵ�ظ���Ϊһ")
			return false
		end
	end
	table.insert(existList, value)
	return true
end

function Checker.CheckRange(columnName, value, tParams)
	if #tParams ~= 2 then
		print("CheckRange�����б���������")
		return false
	end
	
	if value >= tParams[1] and value <= tParams[2] then
		return true
	end
	
	print(columnName .. "��" .. value .. "ֵ����������" .. tParams[1] .. "," .. tParams[2])
	return false
end

function Checker.CheckIDExistAndCanBeZero(columnName, value, tParams)
	if #tParams ~= 2 then
		print("CheckIDExistAndCanBeZero�����б���������")
		return false
	end
	
	if value == 0 then
		return true
	end
	
	local tCheckTable = tParams[1]
	local szTableKey = tParams[2]
	for nID, tValues in pairs(tCheckTable)
	do
		if tValues[szTableKey] == value then
			return true
		end
	end
	print(columnName .. "��" .. value .. "ֵ���Ҳ�����Ӧ��ֵ" .. szTableKey)
	return false
end



