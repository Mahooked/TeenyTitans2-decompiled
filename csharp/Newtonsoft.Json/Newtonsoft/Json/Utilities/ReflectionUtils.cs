using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace Newtonsoft.Json.Utilities;

internal static class ReflectionUtils
{
	public static readonly Type[] EmptyTypes;

	static ReflectionUtils()
	{
		EmptyTypes = Type.EmptyTypes;
	}

	public static bool IsVirtual(this PropertyInfo propertyInfo)
	{
		ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
		MethodInfo getMethod = propertyInfo.GetGetMethod();
		if (getMethod != null && getMethod.IsVirtual)
		{
			return true;
		}
		getMethod = propertyInfo.GetSetMethod();
		if (getMethod != null && getMethod.IsVirtual)
		{
			return true;
		}
		return false;
	}

	public static MethodInfo GetBaseDefinition(this PropertyInfo propertyInfo)
	{
		ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
		MethodInfo getMethod = propertyInfo.GetGetMethod();
		if (getMethod != null)
		{
			return getMethod.GetBaseDefinition();
		}
		return propertyInfo.GetSetMethod()?.GetBaseDefinition();
	}

	public static bool IsPublic(PropertyInfo property)
	{
		if (property.GetGetMethod() != null && property.GetGetMethod().IsPublic)
		{
			return true;
		}
		if (property.GetSetMethod() != null && property.GetSetMethod().IsPublic)
		{
			return true;
		}
		return false;
	}

	public static Type GetObjectType(object v)
	{
		return v?.GetType();
	}

	public static string GetTypeName(Type t, FormatterAssemblyStyle assemblyFormat, SerializationBinder binder)
	{
		string assemblyQualifiedName = t.AssemblyQualifiedName;
		return assemblyFormat switch
		{
			FormatterAssemblyStyle.Simple => RemoveAssemblyDetails(assemblyQualifiedName), 
			FormatterAssemblyStyle.Full => assemblyQualifiedName, 
			_ => throw new ArgumentOutOfRangeException(), 
		};
	}

	private static string RemoveAssemblyDetails(string fullyQualifiedTypeName)
	{
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = false;
		bool flag2 = false;
		foreach (char c in fullyQualifiedTypeName)
		{
			switch (c)
			{
			case '[':
				flag = false;
				flag2 = false;
				stringBuilder.Append(c);
				break;
			case ']':
				flag = false;
				flag2 = false;
				stringBuilder.Append(c);
				break;
			case ',':
				if (!flag)
				{
					flag = true;
					stringBuilder.Append(c);
				}
				else
				{
					flag2 = true;
				}
				break;
			default:
				if (!flag2)
				{
					stringBuilder.Append(c);
				}
				break;
			}
		}
		return stringBuilder.ToString();
	}

	public static bool HasDefaultConstructor(Type t, bool nonPublic)
	{
		ValidationUtils.ArgumentNotNull(t, "t");
		if (TypeExtensions.IsValueType(t))
		{
			return true;
		}
		return GetDefaultConstructor(t, nonPublic) != null;
	}

	public static ConstructorInfo GetDefaultConstructor(Type t)
	{
		return GetDefaultConstructor(t, nonPublic: false);
	}

	public static ConstructorInfo GetDefaultConstructor(Type t, bool nonPublic)
	{
		BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
		if (nonPublic)
		{
			bindingFlags |= BindingFlags.NonPublic;
		}
		return t.GetConstructors(bindingFlags).SingleOrDefault((ConstructorInfo c) => !c.GetParameters().Any());
	}

	public static bool IsNullable(Type t)
	{
		ValidationUtils.ArgumentNotNull(t, "t");
		if (TypeExtensions.IsValueType(t))
		{
			return IsNullableType(t);
		}
		return true;
	}

	public static bool IsNullableType(Type t)
	{
		ValidationUtils.ArgumentNotNull(t, "t");
		return TypeExtensions.IsGenericType(t) && t.GetGenericTypeDefinition() == typeof(Nullable<>);
	}

	public static Type EnsureNotNullableType(Type t)
	{
		return IsNullableType(t) ? Nullable.GetUnderlyingType(t) : t;
	}

	public static bool IsGenericDefinition(Type type, Type genericInterfaceDefinition)
	{
		if (!TypeExtensions.IsGenericType(type))
		{
			return false;
		}
		Type genericTypeDefinition = type.GetGenericTypeDefinition();
		return genericTypeDefinition == genericInterfaceDefinition;
	}

	public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition)
	{
		Type implementingType;
		return ImplementsGenericDefinition(type, genericInterfaceDefinition, out implementingType);
	}

	public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, out Type implementingType)
	{
		ValidationUtils.ArgumentNotNull(type, "type");
		ValidationUtils.ArgumentNotNull(genericInterfaceDefinition, "genericInterfaceDefinition");
		if (!TypeExtensions.IsInterface(genericInterfaceDefinition) || !TypeExtensions.IsGenericTypeDefinition(genericInterfaceDefinition))
		{
			throw new ArgumentNullException("'{0}' is not a generic interface definition.".FormatWith(CultureInfo.InvariantCulture, genericInterfaceDefinition));
		}
		if (TypeExtensions.IsInterface(type) && TypeExtensions.IsGenericType(type))
		{
			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			if (genericInterfaceDefinition == genericTypeDefinition)
			{
				implementingType = type;
				return true;
			}
		}
		Type[] interfaces = type.GetInterfaces();
		foreach (Type type2 in interfaces)
		{
			if (TypeExtensions.IsGenericType(type2))
			{
				Type genericTypeDefinition2 = type2.GetGenericTypeDefinition();
				if (genericInterfaceDefinition == genericTypeDefinition2)
				{
					implementingType = type2;
					return true;
				}
			}
		}
		implementingType = null;
		return false;
	}

	public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition)
	{
		Type implementingType;
		return InheritsGenericDefinition(type, genericClassDefinition, out implementingType);
	}

	public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition, out Type implementingType)
	{
		ValidationUtils.ArgumentNotNull(type, "type");
		ValidationUtils.ArgumentNotNull(genericClassDefinition, "genericClassDefinition");
		if (!TypeExtensions.IsClass(genericClassDefinition) || !TypeExtensions.IsGenericTypeDefinition(genericClassDefinition))
		{
			throw new ArgumentNullException("'{0}' is not a generic class definition.".FormatWith(CultureInfo.InvariantCulture, genericClassDefinition));
		}
		return InheritsGenericDefinitionInternal(type, genericClassDefinition, out implementingType);
	}

	private static bool InheritsGenericDefinitionInternal(Type currentType, Type genericClassDefinition, out Type implementingType)
	{
		if (TypeExtensions.IsGenericType(currentType))
		{
			Type genericTypeDefinition = currentType.GetGenericTypeDefinition();
			if (genericClassDefinition == genericTypeDefinition)
			{
				implementingType = currentType;
				return true;
			}
		}
		if (TypeExtensions.BaseType(currentType) == null)
		{
			implementingType = null;
			return false;
		}
		return InheritsGenericDefinitionInternal(TypeExtensions.BaseType(currentType), genericClassDefinition, out implementingType);
	}

	public static Type GetCollectionItemType(Type type)
	{
		ValidationUtils.ArgumentNotNull(type, "type");
		if (type.IsArray)
		{
			return type.GetElementType();
		}
		if (ImplementsGenericDefinition(type, typeof(IEnumerable<>), out var implementingType))
		{
			if (TypeExtensions.IsGenericTypeDefinition(implementingType))
			{
				throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, type));
			}
			return implementingType.GetGenericArguments()[0];
		}
		if (typeof(IEnumerable).IsAssignableFrom(type))
		{
			return null;
		}
		throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, type));
	}

	public static void GetDictionaryKeyValueTypes(Type dictionaryType, out Type keyType, out Type valueType)
	{
		ValidationUtils.ArgumentNotNull(dictionaryType, "dictionaryType");
		if (ImplementsGenericDefinition(dictionaryType, typeof(IDictionary<, >), out var implementingType))
		{
			if (TypeExtensions.IsGenericTypeDefinition(implementingType))
			{
				throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, dictionaryType));
			}
			Type[] genericArguments = implementingType.GetGenericArguments();
			keyType = genericArguments[0];
			valueType = genericArguments[1];
		}
		else
		{
			if (!typeof(IDictionary).IsAssignableFrom(dictionaryType))
			{
				throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, dictionaryType));
			}
			keyType = null;
			valueType = null;
		}
	}

	public static Type GetMemberUnderlyingType(MemberInfo member)
	{
		ValidationUtils.ArgumentNotNull(member, "member");
		return TypeExtensions.MemberType(member) switch
		{
			MemberTypes.Field => ((FieldInfo)member).FieldType, 
			MemberTypes.Property => ((PropertyInfo)member).PropertyType, 
			MemberTypes.Event => ((EventInfo)member).EventHandlerType, 
			MemberTypes.Method => ((MethodInfo)member).ReturnType, 
			_ => throw new ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo, EventInfo or MethodInfo", "member"), 
		};
	}

	public static bool IsIndexedProperty(MemberInfo member)
	{
		ValidationUtils.ArgumentNotNull(member, "member");
		if (member is PropertyInfo property)
		{
			return IsIndexedProperty(property);
		}
		return false;
	}

	public static bool IsIndexedProperty(PropertyInfo property)
	{
		ValidationUtils.ArgumentNotNull(property, "property");
		return property.GetIndexParameters().Length != 0;
	}

	public static object GetMemberValue(MemberInfo member, object target)
	{
		ValidationUtils.ArgumentNotNull(member, "member");
		ValidationUtils.ArgumentNotNull(target, "target");
		switch (TypeExtensions.MemberType(member))
		{
		case MemberTypes.Field:
			return ((FieldInfo)member).GetValue(target);
		case MemberTypes.Property:
			try
			{
				return ((PropertyInfo)member).GetValue(target, null);
			}
			catch (TargetParameterCountException innerException)
			{
				throw new ArgumentException("MemberInfo '{0}' has index parameters".FormatWith(CultureInfo.InvariantCulture, member.Name), innerException);
			}
		default:
			throw new ArgumentException("MemberInfo '{0}' is not of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, CultureInfo.InvariantCulture, member.Name), "member");
		}
	}

	public static void SetMemberValue(MemberInfo member, object target, object value)
	{
		ValidationUtils.ArgumentNotNull(member, "member");
		ValidationUtils.ArgumentNotNull(target, "target");
		switch (TypeExtensions.MemberType(member))
		{
		case MemberTypes.Field:
			((FieldInfo)member).SetValue(target, value);
			break;
		case MemberTypes.Property:
			((PropertyInfo)member).SetValue(target, value, null);
			break;
		default:
			throw new ArgumentException("MemberInfo '{0}' must be of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, member.Name), "member");
		}
	}

	public static bool CanReadMemberValue(MemberInfo member, bool nonPublic)
	{
		switch (TypeExtensions.MemberType(member))
		{
		case MemberTypes.Field:
		{
			FieldInfo fieldInfo = (FieldInfo)member;
			if (nonPublic)
			{
				return true;
			}
			if (fieldInfo.IsPublic)
			{
				return true;
			}
			return false;
		}
		case MemberTypes.Property:
		{
			PropertyInfo propertyInfo = (PropertyInfo)member;
			if (!propertyInfo.CanRead)
			{
				return false;
			}
			if (nonPublic)
			{
				return true;
			}
			return propertyInfo.GetGetMethod(nonPublic) != null;
		}
		default:
			return false;
		}
	}

	public static bool CanSetMemberValue(MemberInfo member, bool nonPublic, bool canSetReadOnly)
	{
		switch (TypeExtensions.MemberType(member))
		{
		case MemberTypes.Field:
		{
			FieldInfo fieldInfo = (FieldInfo)member;
			if (fieldInfo.IsLiteral)
			{
				return false;
			}
			if (fieldInfo.IsInitOnly && !canSetReadOnly)
			{
				return false;
			}
			if (nonPublic)
			{
				return true;
			}
			if (fieldInfo.IsPublic)
			{
				return true;
			}
			return false;
		}
		case MemberTypes.Property:
		{
			PropertyInfo propertyInfo = (PropertyInfo)member;
			if (!propertyInfo.CanWrite)
			{
				return false;
			}
			if (nonPublic)
			{
				return true;
			}
			return propertyInfo.GetSetMethod(nonPublic) != null;
		}
		default:
			return false;
		}
	}

	public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttr)
	{
		List<MemberInfo> list = new List<MemberInfo>();
		list.AddRange(GetFields(type, bindingAttr));
		list.AddRange(GetProperties(type, bindingAttr));
		List<MemberInfo> list2 = new List<MemberInfo>(list.Count);
		foreach (IGrouping<string, MemberInfo> item in from m in list
			group m by m.Name)
		{
			int num = item.Count();
			IList<MemberInfo> list3 = item.ToList();
			if (num == 1)
			{
				list2.Add(list3.First());
				continue;
			}
			IList<MemberInfo> list4 = new List<MemberInfo>();
			foreach (MemberInfo item2 in list3)
			{
				if (list4.Count == 0)
				{
					list4.Add(item2);
				}
				else if (!IsOverridenGenericMember(item2, bindingAttr) || item2.Name == "Item")
				{
					list4.Add(item2);
				}
			}
			list2.AddRange(list4);
		}
		return list2;
	}

	private static bool IsOverridenGenericMember(MemberInfo memberInfo, BindingFlags bindingAttr)
	{
		if (TypeExtensions.MemberType(memberInfo) != MemberTypes.Property)
		{
			return false;
		}
		PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
		if (!propertyInfo.IsVirtual())
		{
			return false;
		}
		Type declaringType = propertyInfo.DeclaringType;
		if (!TypeExtensions.IsGenericType(declaringType))
		{
			return false;
		}
		Type genericTypeDefinition = declaringType.GetGenericTypeDefinition();
		if (genericTypeDefinition == null)
		{
			return false;
		}
		MemberInfo[] member = genericTypeDefinition.GetMember(propertyInfo.Name, bindingAttr);
		if (member.Length == 0)
		{
			return false;
		}
		Type memberUnderlyingType = GetMemberUnderlyingType(member[0]);
		if (!memberUnderlyingType.IsGenericParameter)
		{
			return false;
		}
		return true;
	}

	public static T GetAttribute<T>(object attributeProvider) where T : Attribute
	{
		return GetAttribute<T>(attributeProvider, inherit: true);
	}

	public static T GetAttribute<T>(object attributeProvider, bool inherit) where T : Attribute
	{
		T[] attributes = GetAttributes<T>(attributeProvider, inherit);
		return (attributes != null) ? attributes.FirstOrDefault() : null;
	}

	public static T[] GetAttributes<T>(object attributeProvider, bool inherit) where T : Attribute
	{
		Attribute[] attributes = GetAttributes(attributeProvider, typeof(T), inherit);
		if (attributes is T[] result)
		{
			return result;
		}
		return attributes.Cast<T>().ToArray();
	}

	public static Attribute[] GetAttributes(object attributeProvider, Type attributeType, bool inherit)
	{
		ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
		if (attributeProvider is Type)
		{
			Type type = (Type)attributeProvider;
			object[] source = ((attributeType != null) ? type.GetCustomAttributes(attributeType, inherit) : type.GetCustomAttributes(inherit));
			Attribute[] array = source.Cast<Attribute>().ToArray();
			if (inherit && type.BaseType != null)
			{
				array = array.Union(GetAttributes(type.BaseType, attributeType, inherit)).ToArray();
			}
			return array;
		}
		if (attributeProvider is Assembly)
		{
			Assembly element = (Assembly)attributeProvider;
			return (attributeType != null) ? Attribute.GetCustomAttributes(element, attributeType) : Attribute.GetCustomAttributes(element);
		}
		if (attributeProvider is MemberInfo)
		{
			MemberInfo element2 = (MemberInfo)attributeProvider;
			return (attributeType != null) ? Attribute.GetCustomAttributes(element2, attributeType, inherit) : Attribute.GetCustomAttributes(element2, inherit);
		}
		if (attributeProvider is Module)
		{
			Module element3 = (Module)attributeProvider;
			return (attributeType != null) ? Attribute.GetCustomAttributes(element3, attributeType, inherit) : Attribute.GetCustomAttributes(element3, inherit);
		}
		if (attributeProvider is ParameterInfo)
		{
			ParameterInfo element4 = (ParameterInfo)attributeProvider;
			return (attributeType != null) ? Attribute.GetCustomAttributes(element4, attributeType, inherit) : Attribute.GetCustomAttributes(element4, inherit);
		}
		ICustomAttributeProvider customAttributeProvider = (ICustomAttributeProvider)attributeProvider;
		object[] array2 = ((attributeType != null) ? customAttributeProvider.GetCustomAttributes(attributeType, inherit) : customAttributeProvider.GetCustomAttributes(inherit));
		return (Attribute[])array2;
	}

	public static void SplitFullyQualifiedTypeName(string fullyQualifiedTypeName, out string typeName, out string assemblyName)
	{
		int? assemblyDelimiterIndex = GetAssemblyDelimiterIndex(fullyQualifiedTypeName);
		if (assemblyDelimiterIndex.HasValue)
		{
			typeName = fullyQualifiedTypeName.Substring(0, assemblyDelimiterIndex.GetValueOrDefault()).Trim();
			assemblyName = fullyQualifiedTypeName.Substring(assemblyDelimiterIndex.GetValueOrDefault() + 1, fullyQualifiedTypeName.Length - assemblyDelimiterIndex.GetValueOrDefault() - 1).Trim();
		}
		else
		{
			typeName = fullyQualifiedTypeName;
			assemblyName = null;
		}
	}

	private static int? GetAssemblyDelimiterIndex(string fullyQualifiedTypeName)
	{
		int num = 0;
		for (int i = 0; i < fullyQualifiedTypeName.Length; i++)
		{
			switch (fullyQualifiedTypeName[i])
			{
			case '[':
				num++;
				break;
			case ']':
				num--;
				break;
			case ',':
				if (num == 0)
				{
					return i;
				}
				break;
			}
		}
		return null;
	}

	public static MemberInfo GetMemberInfoFromType(Type targetType, MemberInfo memberInfo)
	{
		MemberTypes memberTypes = TypeExtensions.MemberType(memberInfo);
		if (memberTypes == MemberTypes.Property)
		{
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			Type[] types = (from p in propertyInfo.GetIndexParameters()
				select p.ParameterType).ToArray();
			return targetType.GetProperty(propertyInfo.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, propertyInfo.PropertyType, types, null);
		}
		return targetType.GetMember(memberInfo.Name, TypeExtensions.MemberType(memberInfo), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).SingleOrDefault();
	}

	public static IEnumerable<FieldInfo> GetFields(Type targetType, BindingFlags bindingAttr)
	{
		ValidationUtils.ArgumentNotNull(targetType, "targetType");
		List<MemberInfo> list = new List<MemberInfo>(targetType.GetFields(bindingAttr));
		GetChildPrivateFields(list, targetType, bindingAttr);
		return list.Cast<FieldInfo>();
	}

	private static void GetChildPrivateFields(IList<MemberInfo> initialFields, Type targetType, BindingFlags bindingAttr)
	{
		if ((bindingAttr & BindingFlags.NonPublic) == 0)
		{
			return;
		}
		BindingFlags bindingAttr2 = bindingAttr.RemoveFlag(BindingFlags.Public);
		while ((targetType = TypeExtensions.BaseType(targetType)) != null)
		{
			IEnumerable<MemberInfo> collection = (from f in targetType.GetFields(bindingAttr2)
				where f.IsPrivate
				select f).Cast<MemberInfo>();
			initialFields.AddRange(collection);
		}
	}

	public static IEnumerable<PropertyInfo> GetProperties(Type targetType, BindingFlags bindingAttr)
	{
		ValidationUtils.ArgumentNotNull(targetType, "targetType");
		List<PropertyInfo> list = new List<PropertyInfo>(targetType.GetProperties(bindingAttr));
		if (TypeExtensions.IsInterface(targetType))
		{
			Type[] interfaces = targetType.GetInterfaces();
			foreach (Type type in interfaces)
			{
				list.AddRange(type.GetProperties(bindingAttr));
			}
		}
		GetChildPrivateProperties(list, targetType, bindingAttr);
		for (int j = 0; j < list.Count; j++)
		{
			PropertyInfo propertyInfo = list[j];
			if (propertyInfo.DeclaringType != targetType)
			{
				PropertyInfo value = (PropertyInfo)GetMemberInfoFromType(propertyInfo.DeclaringType, propertyInfo);
				list[j] = value;
			}
		}
		return list;
	}

	public static BindingFlags RemoveFlag(this BindingFlags bindingAttr, BindingFlags flag)
	{
		return ((bindingAttr & flag) == flag) ? (bindingAttr ^ flag) : bindingAttr;
	}

	private static void GetChildPrivateProperties(IList<PropertyInfo> initialProperties, Type targetType, BindingFlags bindingAttr)
	{
		while ((targetType = TypeExtensions.BaseType(targetType)) != null)
		{
			PropertyInfo[] properties = targetType.GetProperties(bindingAttr);
			foreach (PropertyInfo propertyInfo in properties)
			{
				PropertyInfo subTypeProperty = propertyInfo;
				if (!IsPublic(subTypeProperty))
				{
					int num = initialProperties.IndexOf((PropertyInfo p) => p.Name == subTypeProperty.Name);
					if (num == -1)
					{
						initialProperties.Add(subTypeProperty);
						continue;
					}
					PropertyInfo property = initialProperties[num];
					if (!IsPublic(property))
					{
						initialProperties[num] = subTypeProperty;
					}
				}
				else if (!subTypeProperty.IsVirtual())
				{
					int num2 = initialProperties.IndexOf((PropertyInfo p) => p.Name == subTypeProperty.Name && p.DeclaringType == subTypeProperty.DeclaringType);
					if (num2 == -1)
					{
						initialProperties.Add(subTypeProperty);
					}
				}
				else
				{
					int num3 = initialProperties.IndexOf((PropertyInfo p) => p.Name == subTypeProperty.Name && p.IsVirtual() && p.GetBaseDefinition() != null && p.GetBaseDefinition().DeclaringType.IsAssignableFrom(subTypeProperty.GetBaseDefinition().DeclaringType));
					if (num3 == -1)
					{
						initialProperties.Add(subTypeProperty);
					}
				}
			}
		}
	}

	public static bool IsMethodOverridden(Type currentType, Type methodDeclaringType, string method)
	{
		return currentType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any((MethodInfo info) => info.Name == method && info.DeclaringType != methodDeclaringType && info.GetBaseDefinition().DeclaringType == methodDeclaringType);
	}

	public static object GetDefaultValue(Type type)
	{
		if (!TypeExtensions.IsValueType(type))
		{
			return null;
		}
		switch (ConvertUtils.GetTypeCode(type))
		{
		case PrimitiveTypeCode.Boolean:
			return false;
		case PrimitiveTypeCode.Char:
		case PrimitiveTypeCode.SByte:
		case PrimitiveTypeCode.Int16:
		case PrimitiveTypeCode.UInt16:
		case PrimitiveTypeCode.Int32:
		case PrimitiveTypeCode.Byte:
		case PrimitiveTypeCode.UInt32:
			return 0;
		case PrimitiveTypeCode.Int64:
		case PrimitiveTypeCode.UInt64:
			return 0L;
		case PrimitiveTypeCode.Single:
			return 0f;
		case PrimitiveTypeCode.Double:
			return 0.0;
		case PrimitiveTypeCode.Decimal:
			return 0m;
		case PrimitiveTypeCode.DateTime:
			return default(DateTime);
		case PrimitiveTypeCode.Guid:
			return default(Guid);
		case PrimitiveTypeCode.DateTimeOffset:
			return default(DateTimeOffset);
		default:
			if (IsNullable(type))
			{
				return null;
			}
			return Activator.CreateInstance(type);
		}
	}
}
