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
		print(checkerName .. "表格名称填写错误，不存在此表")
		return
	end
	
	if tChecker.tCheckList == nil or #tChecker.tCheckList == 0 then
		print(checkerName .. "检查项为空")
		return
	end
	
	Checker.Reset()
	
	print(checkerName .. "检查完成！")
	
	for nID, tValues in pairs(tChecker.tTarget)
	do
		for nIndex, tCheckItem in ipairs(tChecker.tCheckList)
		do
			if tCheckItem.funName == nil then
				print(checkerName .. "项" .. nIndex .. "不存在")
				return
			end
			local fun = Checker[tCheckItem.funName]
			
			local tValue = nil
			local szColumnName = nil
			local tCurValues = tValues
			if type(tCheckItem.column) == "table" then --说明需要从struct或者list里面去查找
				for _,columnName in ipairs(tCheckItem.column)
				do
					szColumnName = columnName
					tValue = tCurValues[columnName]
					if tValue == nil then -- 当直接查找不到key值，有可能这是个map
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
							print("ID:" .. nID .. ":" .. checkerName .. "项" .. nIndex .. "列不存在" .. szColumnName)
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
				--值可能为list<int>,list<string>,list<float>类型
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
			print(columnName .. "列" .. value .. "值重复不为一")
			return false
		end
	end
	table.insert(existList, value)
	return true
end

function Checker.CheckRange(columnName, value, tParams)
	if #tParams ~= 2 then
		print("CheckRange参数列表不等于两个")
		return false
	end
	
	if value >= tParams[1] and value <= tParams[2] then
		return true
	end
	
	print(columnName .. "列" .. value .. "值不在区间内" .. tParams[1] .. "," .. tParams[2])
	return false
end

function Checker.CheckIDExistAndCanBeZero(columnName, value, tParams)
	if #tParams ~= 2 then
		print("CheckIDExistAndCanBeZero参数列表不等于两个")
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
	print(columnName .. "列" .. value .. "值查找不到对应键值" .. szTableKey)
	return false
end



